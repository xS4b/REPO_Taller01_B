using System;
using System.IO;
using System.Text;

namespace TallerProgramacion {
    class Program {
        static void Main(string[] args) {
            
            Console.Title = "Práctica de Ingeniería de Archivos - IUJO";
            Console.WriteLine("REPORTE DE EJECUCIÓN");

            // TAREA 1: VALIDACIÓN DE CREDENCIALES
            string inputUsuario = "user_test;password123"; 
            string[] datosSeparados = inputUsuario.Split(';');

            // Verificamos seguridad de la contraseña
            if (datosSeparados.Length == 2 && datosSeparados[1].Contains("123")) {
                // Guardamos el log de error
                File.AppendAllText("seguridad.txt", "Alerta: Contraseña insegura detectada para " + datosSeparados[0] + Environment.NewLine);
                Console.WriteLine("Desafío 1: Aviso de seguridad registrado.");
            }

            // TAREA 2: DUPLICACIÓN BINARIA
            string pathOriginal = "avatar.jpg";
            string pathCopia = "copia_seguridad.jpg";

            // Aseguramos que el origen exista para evitar crashes
            if (!File.Exists(pathOriginal)) {
                File.WriteAllBytes(pathOriginal, new byte[] { 0x0, 0x1, 0x2 }); 
            }

            // Implementación con flujo de datos
            using (var lector = new FileStream(pathOriginal, FileMode.Open))
            using (var escritor = new FileStream(pathCopia, FileMode.Create)) {
                byte[] bloque = new byte[1024];
                int bytesLeidos;
                
                // Bucle de copiado
                while ((bytesLeidos = lector.Read(bloque, 0, bloque.Length)) != 0) {
                    escritor.Write(bloque, 0, bytesLeidos);
                }
            }
            Console.WriteLine("Desafío 2: Duplicación finalizada con éxito.");

            //TAREA 3: FILTRO DE ALMACENAMIENTO
            // Usamos DirectoryInfo en lugar de Directory para variar la técnica
            DirectoryInfo directorioApp = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] archivosEncontrados = directorioApp.GetFiles();

            Console.WriteLine("\nEscaneando archivos pesados...");
            foreach (var item in archivosEncontrados) {
                // Limite de 5KB (5120 bytes)
                if (item.Length > 5120) {
                    Console.WriteLine("Detectado: {0} | Tamaño: {1} KB", item.Name, item.Length / 1024);
                    // item.Delete(); // Acción de borrado opcional
                }
            }

            Console.WriteLine("\nProceso terminado. Enter para cerrar.");
            Console.ReadLine();
        }
    }
}