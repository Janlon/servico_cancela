// Decompiled with JetBrains decompiler
// Type: Infra.BaseInfra
// Assembly: Infra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4930A2D8-6462-4B91-93A8-EA33E1212BC7
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\G01CNC01\Infra.dll

using System.Configuration;

namespace Infra
{
  public class BaseInfra : AcessoDados.AcessoDados
  {
    public string v_Aplicacao = ConfigurationManager.AppSettings["APLICACAO"].ToString();
  }
}
