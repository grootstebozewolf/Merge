using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Reflection.Emit;

namespace SDILReader
{
    public class ILInstruction
    {

        public class EventArgsSqlInScript : EventArgs
        {
            public object Operand;
            public object Instructions;
            public int StackPointer;
        }
        public delegate void SqlInScriptEventHandler(Object sender, EventArgsSqlInScript e);
        public event SqlInScriptEventHandler OnSqlInScript;

        // Fields
        private OpCode code;
        private object operand;
        private byte[] operandData;
        private int offset;

        // Properties
        public OpCode Code
        {
            get { return code; }
            set { code = value; }
        }

        public object Operand
        {
            get { return operand; }
            set { operand = value; }
        }

        public byte[] OperandData
        {
            get { return operandData; }
            set { operandData = value; }
        }

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Returns a friendly strign representation of this instruction
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            //string result = "";
            //result += GetExpandedOffset(offset) + " : " + code;
            //if (operand != null)
            //{
            //    switch (code.OperandType)
            //    {
            //        case OperandType.InlineField:
            //            System.Reflection.FieldInfo fOperand = ((System.Reflection.FieldInfo)operand);
            //            result += " " + Globals.ProcessSpecialTypes(fOperand.FieldType.ToString()) + " " +
            //                Globals.ProcessSpecialTypes(fOperand.ReflectedType.ToString()) +
            //                "::" + fOperand.Name + "";
            //            break;
            //        case OperandType.InlineMethod:
            //            try
            //            {
            //                System.Reflection.MethodInfo mOperand = (System.Reflection.MethodInfo)operand;
            //                result += " ";
            //                if (!mOperand.IsStatic) result += "instance ";
            //                result += Globals.ProcessSpecialTypes(mOperand.ReturnType.ToString()) +
            //                    " " + Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
            //                    "::" + mOperand.Name + "()";
            //                switch (((MethodInfo)operand).Name)
            //                {
            //                    case "DoNCSql":
            //                    case "ExecuteScalarSQL":
            //                        Console.WriteLine(instructions[instructions.Count - 1].Operand);
            //                        OnSqlInScript(this, new EventArgsSqlInScript { Instructions = result, Operand = instructions[instructions.Count - 1].Operand });
            //                        break;
            //                    default:
            //                        break;
            //                }
            //            }
            //            catch
            //            {
            //                try
            //                {
            //                    System.Reflection.ConstructorInfo mOperand = (System.Reflection.ConstructorInfo)operand;
            //                    result += " ";
            //                    if (!mOperand.IsStatic) result += "instance ";
            //                    result += "void " +
            //                        Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
            //                        "::" + mOperand.Name + "()";
            //                }
            //                catch
            //                {
            //                }
            //            }
            //            break;
            //        case OperandType.ShortInlineBrTarget:
            //        case OperandType.InlineBrTarget:
            //            result += " " + GetExpandedOffset((int)operand);
            //            break;
            //        case OperandType.InlineType:
            //            result += " " + Globals.ProcessSpecialTypes(operand.ToString());
            //            break;
            //        case OperandType.InlineString:
            //            if (operand.ToString() == "\r\n") result += " \"\\r\\n\"";
            //            else result += " \"" + operand.ToString() + "\"";
            //            break;
            //        case OperandType.ShortInlineVar:
            //            result += operand.ToString();
            //            break;
            //        case OperandType.InlineI:
            //        case OperandType.InlineI8:
            //        case OperandType.InlineR:
            //        case OperandType.ShortInlineI:
            //        case OperandType.ShortInlineR:
            //            result += operand.ToString();
            //            break;
            //        case OperandType.InlineTok:
            //            if (operand is Type)
            //                result += ((Type)operand).FullName;
            //            else
            //                result += "not supported";
            //            break;

            //        default: result += "not supported"; break;
            //    }
            //}
            //return result;
            return GetCode(null, 0);
        }

        /// <summary>
        /// Add enough zeros to a number as to be represented on 4 characters
        /// </summary>
        /// <param name="offset">
        /// The number that must be represented on 4 characters
        /// </param>
        /// <returns>
        /// </returns>
        private string GetExpandedOffset(long offset)
        {
            string result = offset.ToString();
            for (int i = 0; result.Length < 4; i++)
            {
                result = "0" + result;
            }
            return result;
        }

        public ILInstruction()
        {

        }

        public string GetCode(List<ILInstruction> instructions, int i)
        {
            string result = "";
            result += GetExpandedOffset(offset) + " : " + code;
            if (operand != null)
            {
                switch (code.OperandType)
                {
                    case OperandType.InlineField:
                        System.Reflection.FieldInfo fOperand = ((System.Reflection.FieldInfo)operand);
                        result += " " + Globals.ProcessSpecialTypes(fOperand.FieldType.ToString()) + " " +
                            Globals.ProcessSpecialTypes(fOperand.ReflectedType.ToString()) +
                            "::" + fOperand.Name + "";
                        break;
                    case OperandType.InlineMethod:
                        try
                        {
                            System.Reflection.MethodInfo mOperand = (System.Reflection.MethodInfo)operand;
                            result += " ";
                            if (!mOperand.IsStatic) result += "instance ";
                            result += Globals.ProcessSpecialTypes(mOperand.ReturnType.ToString()) +
                                " " + Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                "::" + mOperand.Name + "()";
                            switch (mOperand.Name)
                            {
                                case "DoNCSql":
                                case "ExecuteScalarSQL":
                                    Console.WriteLine(instructions[instructions.Count - 1].Operand);
                                    OnSqlInScript(this, new EventArgsSqlInScript { Instructions = instructions, Operand = instructions[i - 1].Operand, StackPointer =  i});
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch
                        {
                            try
                            {
                                System.Reflection.ConstructorInfo mOperand = (System.Reflection.ConstructorInfo)operand;
                                result += " ";
                                if (!mOperand.IsStatic) result += "instance ";
                                result += "void " +
                                    Globals.ProcessSpecialTypes(mOperand.ReflectedType.ToString()) +
                                    "::" + mOperand.Name + "()";
                            }
                            catch
                            {
                            }
                        }
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        result += " " + GetExpandedOffset((int)operand);
                        break;
                    case OperandType.InlineType:
                        result += " " + Globals.ProcessSpecialTypes(operand.ToString());
                        break;
                    case OperandType.InlineString:
                        if (operand.ToString() == "\r\n") result += " \"\\r\\n\"";
                        else result += " \"" + operand.ToString() + "\"";
                        break;
                    case OperandType.ShortInlineVar:
                        result += operand.ToString();
                        break;
                    case OperandType.InlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineR:
                        result += operand.ToString();
                        break;
                    case OperandType.InlineTok:
                        if (operand is Type)
                            result += ((Type)operand).FullName;
                        else
                            result += "not supported";
                        break;

                    default: result += "not supported"; break;
                }
            }
            return result;
        }
    }
}
