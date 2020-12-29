@rem Detecting Administrative permissions...
@
@net session >nul 2>&1
@  if %errorLevel% == 0 (
@    rem echo Success: Administrative permissions confirmed.
@    rem pause >nul
  ) else (
@    rem echo Failure: Current permissions inadequate.
@    rem pause >nul
@    goto NOT_ADMIN
  )
@
:INICIO
@rem AMARELO NO VERMELHO
@color CE
@CLS
@echo .
@echo .
@echo .
@echo .     VAI MATAR E REMOVER SERVICO:  G03CNC01
@echo .
@echo .
@echo .
@PAUSE
@rem PRETO NO AMARELO
@color E0
@CLS
@echo .
@     taskkill /f /fi "SERVICES eq G03CNC01"
@echo .
@     sc delete "G03CNC01" 
@echo .
@echo .
@echo .
@echo .
@ECHO .     --- SERVICO DEVE ESTAR MORTO... RECOMPILE C# AGORA!!!
@echo .
@echo .
@echo .
@echo .
@PAUSE
@rem AMARELO NO VERDE
@color 2E
@CLS
@echo .
@echo .
@echo .
@ECHO .     VAI CRIAR E INICIAR SERVICO:  G03CNC01
@echo .
@echo .
@echo .
@rem sc create "G03CNC01" binpath= "C:\Users\adm\source\repos\ServicoTorniqueteFull\G01CNC01\bin\Debug\AcessoTorniquete.exe" displayname= "G03CNC01"
@rem sc create "G03CNC01" binpath= "E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\__ServicoTorniqueteFull\G01CNC01\bin\Debug\AcessoTorniquete.exe" displayname= "G03CNC01"
     sc create "G03CNC01" binpath= "E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\__ServicoCancelaFull\G01CNC01\bin\Debug\AcessoVeiculos.exe"      displayname= "G03CNC01"

@echo .
@echo .
@echo .
     sc start G03CNC01
@echo .
@echo .
@echo .
@echo .
@echo .
@echo .
@ECHO .     FIM OK!!! (?)
@echo .
@echo .
@echo .
@echo .
@echo .
@echo .
@PAUSE
@GOTO INICIO

:NOT_ADMIN
@rem AMARELO NO VERMELHO
@color CE
@CLS
@echo .
@echo .
@echo .
@echo .
@echo .     Rode este BAT com permissao de ADMIN!
@echo .
@echo .
@echo .
@echo .
@PAUSE
