using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anti_Debugger_Simples
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Lista de todos os programas depuradores
        /// </summary>
        readonly static string[] DebuggersNames = {
            "x32dbg",
            "!x32dbg",
            "x64dbg",
            "!x64dbg",
            "OllyDbg",
            "ida",
            "ida64",
            "ida -",
            "ida64 -",
            "IMMUNITYDEBUGGER",
            "codecracker",
            "x96dbg",
            "de4dot",
            "ilspy",
            "graywolf",
            "die",
            "simpleassemblyexplorer",
            "megadumper",
            "x64netdumper",
            "hxd",
            "petools",
            "protection_id",
            "charles",
            "dnspy",
            "simpleassembly",
            "peek",
            "httpanalyzer",
            "httpdebug",
            "fiddler",
            "wireshark",
            "proxifier",
            "mitmproxy",
            "processhacker",
            "memoryedit",
            "memoryscanner",
            "memory scanner"
        };

        /// <summary>
        /// Matar um processo Debug através de um arquivo BAT
        /// </summary>
        private async static void MatarDepurador()
        {
            // Novo Random, iremos usar logo
            Random rd = new Random();

            // Nome do arquivo, com um número aleatorio, para impedir que neguem
            // O acesso
            string tempFile = Path.GetTempPath() + "!!teste" + rd.Next(0, 5000) + ".bat";

            // Primeiramente, vamos criar um arquivo
            File.WriteAllText(tempFile, "");

            // Espere um pouco, porque pode dar erro
            await Task.Delay(100);

            // Procure todos os nomes de depuradores
            foreach (string line in DebuggersNames)
            {
                try
                {
                    // Agora, adicione uma linha, dizendo para finalizar os processos
                    // Depuradores
                    File.AppendAllText(
                        // Arquivo
                        tempFile,

                        // Código
                        "taskkill /f /pid " + '"' + line + ".exe" + '"' +
                        Environment.NewLine
                    );
                }
                catch (Exception) { }
            }

            // Agora, adicione o comando pro arquivo bat de auto-deletar
            File.AppendAllText(
                // Arquivo
                tempFile,

                // Código
                "del /f /q " + '"' + tempFile + '"' + Environment.NewLine
            );

            // Novo processo
            Process pp = new Process();

            // Local do arquivo
            pp.StartInfo.FileName = tempFile;
            pp.StartInfo.Arguments = "";

            // Sem janela, faça isso em segundo plano
            pp.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // Inicie o programa
            // Se um depurador for encontrado, o processo será finalizado
            pp.Start();

            // Espere o programa sair
            pp.WaitForExit();

            // Agora, delete o arquivo
            File.Delete(tempFile);
        }

        public Form1()
        {
            InitializeComponent();
            MatarDepurador();
        }

    }
}
