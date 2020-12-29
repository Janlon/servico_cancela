// Decompiled with JetBrains decompiler
// Type: Comum.CriaChaveRegistroWindows
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Microsoft.Win32;
using System;

namespace Comum
{
  public class CriaChaveRegistroWindows : IDisposable
  {
    private bool disposedValue = false;

    public void CriarChaveRegistroWindows(string v_s_Aplicacao)
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CURRENTCONTROLSET\\SERVICES\\EVENTLOG\\APPLICATION\\", true);
      try
      {
        string[] subKeyNames = registryKey.GetSubKeyNames();
        bool flag = true;
        for (int index = 0; index < subKeyNames.Length; ++index)
        {
          if (subKeyNames[index].ToString() == v_s_Aplicacao)
          {
            flag = false;
            registryKey.Dispose();
            break;
          }
        }
        if (!flag)
          return;
        registryKey.CreateSubKey(v_s_Aplicacao);
        registryKey.Dispose();
      }
      finally
      {
        registryKey.Dispose();
        this.Terminate();
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      if (!disposing)
        ;
      this.disposedValue = true;
    }

    ~CriaChaveRegistroWindows()
    {
      this.Dispose(false);
    }

    void IDisposable.Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public void Terminate()
    {
      this.Dispose(true);
    }
  }
}
