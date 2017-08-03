<Query Kind="Program">
  <Namespace>Microsoft.CSharp</Namespace>
  <Namespace>System.CodeDom.Compiler</Namespace>
</Query>

void Main()
{
	Eval("DateTime.Now").Dump();
}

// Define other methods and classes here
public static object Eval(string sCSCode)
{
  CSharpCodeProvider c = new CSharpCodeProvider();
  CompilerParameters cp = new CompilerParameters();

  cp.ReferencedAssemblies.Add("system.dll");

  cp.GenerateInMemory = true;
  cp.GenerateExecutable = false;

  StringBuilder sb = new StringBuilder("");
  sb.Append("using System;\n" );
  sb.Append("namespace CSCodeEvaler { \n");
  sb.Append("public class CSCodeEvaler { \n");
  sb.Append("public object EvalCode() {\n");
  sb.Append("return "+sCSCode+";\n");
  sb.Append("}\n");
  sb.Append("}\n");
  sb.Append("}\n");

  CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
  if( cr.Errors.Count > 0 )
  {
      throw new Exception(cr.Errors[0].ErrorText);
  }

  System.Reflection.Assembly a = cr.CompiledAssembly;
  object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

  Type t = o.GetType();
  MethodInfo mi = t.GetMethod("EvalCode");

  object s = mi.Invoke(o, null);
  return s;

}