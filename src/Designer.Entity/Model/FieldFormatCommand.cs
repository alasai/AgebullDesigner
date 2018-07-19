using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    public class FieldFormatCommand
    {
        /// <summary>
        /// �ֶ��ı�����
        /// </summary>
        public string Fields { get; set; }


        #region �����ı�(CSharp ���� ����)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DoFormatCSharp(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string descript = null, caption = null;
            int next = 0;
            int barket = 0;
            var code = new StringBuilder();
            foreach (var l in lines)
            {
                if (string.IsNullOrEmpty(l))
                    continue;
                var line = l.Trim().TrimEnd(';');
                if (line[0] == '{')
                {
                    barket++;
                    continue;
                }
                if (line[0] != '/' && line[line.Length - 1] == '}')
                {
                    barket--;
                    continue;
                }
                if (barket > 0)
                    continue;
                var baseLine = line.Split(new[] { '{', '=', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (baseLine[0].Contains('('))
                    continue;
                var words = baseLine[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                    continue;
                if (words[0][0] == '/')
                {
                    if (words[1] == "<summary>")
                    {
                        caption = null;
                        next = 1;
                        continue;
                    }
                    if (words[1] == "<remark>")
                    {
                        descript = null;
                        next = 2;
                        continue;
                    }
                    if (words[1][0] == '<')
                    {
                        next = 0;
                        continue;
                    }
                    if (next <= 1)
                        caption = words.Skip(1).LinkToString();
                    if (next == 2)
                        descript = words.Skip(1).LinkToString();
                    continue;
                }
                if (!Char.IsLetter(line[0]))
                    continue;
                var name = words[words.Length - 1];
                var type = words[words.Length - 2];
                code.Append($"{name},{type}");
                if (caption != null)
                    code.Append($",{caption}");
                if (descript != null)
                    code.Append($",{descript}");
                code.AppendLine();
                caption = null;
                descript = null;
                next = 0;
            }

            return code.ToString();
        }

        #endregion

        #region �����ı�(���� ���� ����)

        public string DoFormat2(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                code.AppendLine(words.LinkToString(","));
            }

            return code.ToString();
        }

        #endregion

        #region SQLSERVER  �ֶζ�����ʽ


        public string DoFormatSqlServer(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ' ', '\t', '[', ']', '`', ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                    continue;
                code.Append(words[0]);
                switch (words[1].ToLower())
                {
                    case "int":
                        code.Append(",i");
                        break;
                    case "datetime":
                        code.Append(",DateTime");
                        break;
                    case "bit":
                        code.Append(",bool");
                        break;
                    case "bigint":
                        code.Append(",long");
                        break;
                    case "decimal":
                    case "numbic":
                    case "double":
                    case "float":
                        code.Append(",decimal");
                        break;
                    case "text":
                        code.Append(",ls");
                        break;
                    default: //
                        code.Append(",s");
                        break;
                }

                if (words.Length > 2 && words[2][0] == '(')
                {
                    var len = words[2].Trim('(', ')');
                    code.Append($"-{len}");
                }
                else
                {
                    code.Append("-#");
                }

                var def = words.FirstOrDefault(p => p.IndexOf("default", StringComparison.OrdinalIgnoreCase) >= 0);
                var defs = def?.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (defs?.Length > 1) code.Append($"-{defs[1]}");
                code.Append(",");
                var desc = words.FirstOrDefault(p => p.IndexOf("--", StringComparison.OrdinalIgnoreCase) >= 0);
                code.Append(desc?.Trim('-') ?? words[0]);
                code.AppendLine();
            }

            return code.ToString();
        }

        #endregion

        #region MySql �ֶζ�����ʽ


        public string DoFormatMySql(string arg)
        {
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var code = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ' ', '\t', '[', ']', '`', ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                    continue;
                code.Append(words[0]);
                switch (words[1].ToLower())
                {
                    case "int":
                    case "bool":
                        code.Append(",i,");
                        break;
                    case "datetime":
                        code.Append(",DateTime,");
                        break;
                    case "bit":
                        code.Append(",bool,");
                        break;
                    case "bigint":
                        code.Append(",long,");
                        break;
                    case "decimal":
                    case "double":
                    case "float":
                    case "numbic":
                        code.Append(",decimal,");
                        break;
                    case "text":
                        code.Append(",ls");
                        break;
                    default:
                        code.Append(",s,");
                        break;
                }

                if (words.Length > 2 && words[2][0] == '(')
                {
                    var len = words[2].Trim('(', ')');
                    code.Append($"-{len}");
                }
                else
                {
                    code.Append("-#");
                }
            }

            return code.ToString();
        }


        #endregion

        #region �����ı�

        public List<PropertyConfig> DoCheckFieldes(string arg)
        {
            var columns = new List<PropertyConfig>();
            var lines = Fields.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var idx = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var words = line.Trim().Split(new[] { ',', '��' }, StringSplitOptions.RemoveEmptyEntries);

                var name = words[0].TrimStart('_').ToUWord();
                
                /*
                �ı�˵��:
                * 1 ÿ��Ϊһ������
                * 2 ÿ�������ö��ŷֿ�
                * 3 ��һ������ ��������; �ڶ������� ��������;���������� ˵���ı�
                */
                PropertyConfig column = new PropertyConfig
                {
                    IsPrimaryKey = name.Equals("ID", StringComparison.OrdinalIgnoreCase),
                    ColumnName = name,
                    Name = name,
                    CsType = "string",
                    DbType = "nvarchar"
                };
                column.Option.Index = idx++;
                if (words.Length > 1) CsharpHelper.CheckType(column, words[1]);
                if (words.Length > 2)
                column.Caption = words[2];
                if (words.Length > 3)
                column.Description = words.Length < 4 ? null : words.Skip(3).LinkToString(",");
                var old = columns.FirstOrDefault(p => p != null && p.Name == name);
                if (old != null)
                {
                    columns.Remove(old);
                }
                columns.Add(column);
            }

            return columns;
        }


        #endregion
    }
}