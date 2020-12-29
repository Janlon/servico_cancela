// Decompiled with JetBrains decompiler
// Type: AcessoVeiculos.Program
// Assembly: AcessoVeiculos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6322344E-895F-460F-ACE9-76F49CA4E9C5
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\G01CNC01\G01CNC01.exe

using System.ServiceProcess;

namespace AcessoVeiculos
{
  internal static class Program
  {
    private static void Main()
    {
      ServiceBase.Run(new ServiceBase[1]
      {
        (ServiceBase) new AcessoVeiculos()
      });
    }
  }
}
