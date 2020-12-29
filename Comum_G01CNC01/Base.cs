// Decompiled with JetBrains decompiler
// Type: Comum.Base
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

namespace Comum
{
  public class Base : AcessoDados.AcessoDados
  {
    public static bool Envio_Lista_Iniciado = false;

    public static string v_UltimoComandoLista { get; set; }

    public static string v_ComandoPing { get; set; }

    public static int ContaTestePing { get; set; }

    public static bool ExecutaTestePing { get; set; }
  }
}
