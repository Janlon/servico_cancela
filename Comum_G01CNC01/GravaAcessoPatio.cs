// Decompiled with JetBrains decompiler
// Type: Comum.GravaAcessoPatio
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Data;
using System.Diagnostics;

namespace Comum
{
  public class GravaAcessoPatio : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public void GravarAcessoPatio(
      string v_sentido,
      int v_Flag_Motorista,
      int v_ID_EQUIPAMENTO,
      string v_s_Aplicacao,
      string v_ordemServico,
      long v_idAcesso,
      string v_placa,
      string v_Motorista = "",
      string v_dtEmissao = "",
      string v_placaCarreta1 = "",
      string v_placaCarreta2 = "",
      string v_cliente = "",
      string v_transportadora = "")
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vSentido", (object) v_sentido, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vFlagMotorista", (object) v_Flag_Motorista, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdAcesso", (object) v_idAcesso, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vOrdemServico", (object) v_ordemServico, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vPlaca", (object) v_placa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vMotorista", (object) v_Motorista, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vdtEmissao", (object) v_dtEmissao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vPlacaCarreta1", (object) v_placaCarreta1, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vPlacaCarreta2", (object) v_placaCarreta2, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vCliente", (object) v_cliente, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vTransportadora", (object) v_transportadora, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_AcessoPatioInserir", (object) dynamicParameters, "GravaAcessoPatio.GravarAcessoPatio", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro GravarAcessoPatio() ID Controladora: " + v_ID_EQUIPAMENTO.ToString() + " - " + v_s_Aplicacao, EventLogEntryType.Error, ex);
      }
      finally
      {
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

    ~GravaAcessoPatio()
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
