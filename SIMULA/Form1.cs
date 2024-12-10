using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using static SIMULA.Form1;

namespace SIMULA
{
    public partial class Form1 : Form
    {
        private int MemoriaUtilizada
        {
            get
            {
                // Cada partición ocupada vale 10 MB
                return pilaProcesos.Count(p => p != null) * 10;
            }
        }
        private Dictionary<int, Proceso> diccionarioProcesos = new Dictionary<int, Proceso>();
        private int procesoId = 1;
        private Queue<Proceso> colaProcesos = new Queue<Proceso>();
        private Proceso[] pilaProcesos = new Proceso[20];
        private bool simuladorActivo = false;
        private static Random random = new Random();
        private string ajusteSeleccionado = ""; // Variable para almacenar la opción seleccionada(AJUSTES)

        public Form1()
        {
            InitializeComponent();
            TimerCola.Interval = 3000;//tiempo para generar un proceso
            TimerPila.Interval = 1000;
            InicializarColaLabels();
            InicializarPilaLabels();
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ajusteSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione un ajuste antes de continuar.");
                return; // Si no se ha seleccionado, no continuar
            }

            if (ajusteSeleccionado == "Primer Ajuste")
            {
                TimerCola.Stop();
                TimerPila.Stop();
                simuladorActivo = true;
                TimerCola.Start();
                TimerPila.Start();
                MessageBox.Show("AJUSTE SELECCIONADO");
            }
            else if (ajusteSeleccionado == "Mejor Ajuste")
            {
                TimerCola.Stop();
                TimerPila.Stop();
                simuladorActivo = true;
                TimerCola.Start();
                TimerPila.Start();
                MessageBox.Show("AJUSTE SELECCIONADO");
            }
            else if (ajusteSeleccionado == "Peor Ajuste")
            {
                TimerCola.Stop();
                TimerPila.Stop();
                simuladorActivo = true;
                TimerCola.Start();
                TimerPila.Start();
                MessageBox.Show("AJUSTE SELECCIONADO");
            }
            // Reanudar los procesos en pausa
            for (int i = 0; i < pilaProcesos.Length; i++)
            {
                if (pilaProcesos[i] != null && pilaProcesos[i].EnPausa)
                {
                    // Si el proceso está pausado, reanudarlo
                    pilaProcesos[i].EnPausa = false;
                    pilaLabels[i].BackColor = Color.Red; // Cambiar a color rojo (ejecutando)
                }
            }
        }


        private void BtnDetener_Click(object sender, EventArgs e)
        {
            simuladorActivo = false;
            TimerCola.Stop();
            TimerPila.Stop();

            for (int i = 0; i < pilaProcesos.Length; i++)
            {
                if (pilaProcesos[i] != null)
                {
                    pilaProcesos[i].EnPausa = true; // Marcar el proceso como pausado
                    pilaLabels[i].BackColor = Color.Yellow; // Cambiar el color a amarillo
                }
            }
        }

        //CREADOR DE LOS PROCESOS
        private void TimerCola_Tick_1(object sender, EventArgs e)
        {
            // Verificar si la cola tiene menos de 10 procesos
            if (colaProcesos.Count < 10)
            {
                int[] tamanoOpciones = { 10, 20, 30 }; // Opciones de tamaño en MB
                int mb = tamanoOpciones[random.Next(tamanoOpciones.Length)]; // Tamaño aleatorio

                // Verificar si hay memoria suficiente antes de crear el proceso
                if (MemoriaUtilizada + mb <= 500)
                {
                    // Crear un nuevo nombre de proceso con el tamaño (ID)
                    string procesoNombre = $"Proceso{procesoId} - {mb}MBs";

                    // Crear el nuevo proceso con ID único generado automáticamente, tambien genera tiempo 
                    var nuevoProceso = new Proceso(procesoNombre, random.Next(35, 40), mb);

                    // Guardar el proceso en el diccionario usando el ID como clave
                    diccionarioProcesos.Add(nuevoProceso.Id, nuevoProceso);

                    // Añadir el proceso a la cola
                    colaProcesos.Enqueue(nuevoProceso);
                    procesoId++;
                    ActualizarCola();
                }
                else
                {
                    // No hay suficiente memoria, no se crea el proceso
                   // MessageBox.Show("No hay suficiente memoria para crear el nuevo proceso.");
                }
            }
        }

        //FUNCION PARA DIBUJAR LOS PROCESOS EN LA PILA
        private void TimerPila_Tick_1(object sender, EventArgs e)
        {
            if (simuladorActivo)
            {
                if (ajusteSeleccionado == "Primer Ajuste")
                {
                    AsignarProcesosAPila(); // Asignar procesos a la pila
                    lblEstasEn.Text = "SELECCIONASTE PRIMER AJUSTE";
                    var procesosActualizados = new HashSet<Proceso>();
                    for (int i = 0; i < pilaProcesos.Length; i++)
                    {
                        if (pilaProcesos[i] != null && !procesosActualizados.Contains(pilaProcesos[i]))
                        {
                            var proceso = pilaProcesos[i];

                            if (!proceso.EnPausa) // Solo si el proceso no está en pausa
                            {
                                proceso.Duracion--; // Reducir duración si no está pausado
                                pilaLabels[i].Text = $"{proceso.Nombre} - {proceso.Duracion} segs\n";

                                // Cambiar el color de la partición según el estado del proceso
                                if (proceso.Tamano == 10)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 18);
                                    pilaLabels[i].BackColor = Color.Red; // Cambiar a rojo mientras se ejecuta
                                }
                                else if (proceso.Tamano == 20)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 38);
                                    pilaLabels[i].BackColor = Color.Red;
                                }
                                else if (proceso.Tamano == 30)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 58);
                                    pilaLabels[i].BackColor = Color.Red;
                                }

                                // Si el proceso termina su duración
                                if (proceso.Duracion <= 0)
                                {
                                    pilaProcesos[i] = null;
                                    pilaLabels[i].Text = "10 MB"; // Restablecer el estado de la partición
                                    pilaLabels[i].BackColor = Color.White;
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 18);
                                }
                            }

                            procesosActualizados.Add(proceso);
                        }
                    }

                    ActualizarMemoria(); // Actualizar la memoria utilizada
                }
                else if (ajusteSeleccionado == "Mejor Ajuste")
                {

                    AsignarProcesosAPilaMJ(); // Asignar procesos a la pila
                    lblEstasEn.Text = "SELECCIONASTE MEJOR AJUSTE";
                    // Aquí, agregamos la actualización de duración de los procesos asignados
                    var procesosActualizados = new HashSet<Proceso>();
                    for (int i = 0; i < pilaProcesos.Length; i++)
                    {
                        if (pilaProcesos[i] != null && !procesosActualizados.Contains(pilaProcesos[i]))
                        {
                            var proceso = pilaProcesos[i];

                            if (!proceso.EnPausa) // Solo si el proceso no está en pausa
                            {
                                proceso.Duracion--; // Reducir duración si no está pausado
                                pilaLabels[i].Text = $"{proceso.Nombre} - {proceso.Duracion} segs\n";

                                // Cambiar el color de la partición según el estado del proceso
                                if (proceso.Tamano == 10)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 18);
                                    pilaLabels[i].BackColor = Color.Red; // Cambiar a rojo mientras se ejecuta
                                }
                                else if (proceso.Tamano == 20)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 38);
                                    pilaLabels[i].BackColor = Color.Red;
                                }
                                else if (proceso.Tamano == 30)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 58);
                                    pilaLabels[i].BackColor = Color.Red;
                                }

                                // Si el proceso termina su duración
                                if (proceso.Duracion <= 0)
                                {
                                    pilaProcesos[i] = null;
                                    pilaLabels[i].Text = "10 MB"; // Restablecer el estado de la partición
                                    pilaLabels[i].BackColor = Color.White;
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 18);
                                }
                            }

                            procesosActualizados.Add(proceso);
                        }
                    }

                    ActualizarMemoria(); // Actualizar la memoria utilizada
                }
                if (ajusteSeleccionado == "Peor Ajuste")
                {
                    AsignarProcesosAPilaPA(); // Asignar procesos en el peor ajuste
                    lblEstasEn.Text = "SELECCIONASTE PEOR AJUSTE";
                    var procesosActualizados = new HashSet<Proceso>();
                    for (int i = 0; i < pilaProcesos.Length; i++)
                    {
                        if (pilaProcesos[i] != null && !procesosActualizados.Contains(pilaProcesos[i]))
                        {
                            var proceso = pilaProcesos[i];

                            if (!proceso.EnPausa) // Solo si el proceso no está en pausa
                            {
                                proceso.Duracion--; // Reducir duración si no está pausado
                                pilaLabels[i].Text = $"{proceso.Nombre} - {proceso.Duracion} segs\n";

                                // Cambiar el color de la partición según el estado del proceso
                                if (proceso.Tamano == 10)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 18);
                                    pilaLabels[i].BackColor = Color.Red; // Cambiar a rojo mientras se ejecuta
                                }
                                else if (proceso.Tamano == 20)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 38);
                                    pilaLabels[i].BackColor = Color.Red;
                                }
                                else if (proceso.Tamano == 30)
                                {
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 58);
                                    pilaLabels[i].BackColor = Color.Red;
                                }

                                // Si el proceso termina su duración
                                if (proceso.Duracion <= 0)
                                {
                                    pilaProcesos[i] = null;
                                    pilaLabels[i].Text = "10 MB"; // Restablecer el estado de la partición
                                    pilaLabels[i].BackColor = Color.White;
                                    pilaLabels[i].Size = new System.Drawing.Size(200, 18);
                                }
                            }

                            procesosActualizados.Add(proceso);
                        }
                    }

                    ActualizarMemoria(); // Actualizar la memoria utilizada
                }
            }
        }



        //PRIMER AJUSTE
        private void AsignarProcesosAPila()
        {
            while (colaProcesos.Count > 0)
            {
                var proceso = colaProcesos.Peek();
                int particionesRequeridas = proceso.Tamano / 10;

                // Verificar si hay espacio continuo suficiente en la pila
                int indiceInicio = EncontrarEspacioContinuo(particionesRequeridas);

                if (indiceInicio != -1)
                {
                    // Asignar el proceso a las particiones disponibles
                    for (int i = 0; i < particionesRequeridas; i++)
                    {
                        pilaProcesos[indiceInicio + i] = proceso;
                        //pilaLabels[indiceInicio + i].Text = $"{proceso.Nombre}\n{proceso.Duracion} segs\n";
                        //pilaLabels[indiceInicio + i].BackColor = Color.Yellow;

                    }

                    colaProcesos.Dequeue();
                    ActualizarCola();
                    ActualizarMemoria();
                }
                else
                {
                    // Si no hay espacio suficiente, salir del bucle
                    break;
                }
            }
        }

        private int EncontrarEspacioContinuo(int particionesRequeridas)
        {
            int espacioContinuo = 0;

            for (int i = 0; i < pilaProcesos.Length; i++)
            {
                if (pilaProcesos[i] == null)
                {
                    espacioContinuo++;
                    if (espacioContinuo == particionesRequeridas)
                        return i - particionesRequeridas + 1;
                }
                else
                {
                    espacioContinuo = 0;
                }
            }

            return -1; // No hay espacio suficiente y continuo
        }

        //MEJOR AJUSTE
        private void AsignarProcesosAPilaMJ()
        {
            while (colaProcesos.Count > 0)
            {
                var proceso = colaProcesos.Peek();
                int particionesRequeridas = proceso.Tamano / 10;

                // Verificar si hay suficiente espacio en memoria
                int indiceMejorAjuste = EncontrarMejorAjuste(particionesRequeridas);

                if (indiceMejorAjuste != -1)
                {
                    // Asignar el proceso a las particiones disponibles
                    for (int i = 0; i < particionesRequeridas; i++)
                    {
                        pilaProcesos[indiceMejorAjuste + i] = proceso;
                        // Aquí puedes actualizar los labels de la pila para reflejar el proceso asignado
                        //pilaLabels[indiceMejorAjuste + i].BackColor = Color.Red;
                        //pilaLabels[indiceMejorAjuste + i].Text = $"{proceso.Nombre} - {proceso.Duracion} segs\n";

                    }

                    colaProcesos.Dequeue();
                    ActualizarCola();
                    ActualizarMemoria();
                }
                else
                {
                    // Si no hay espacio suficiente, salir del bucle
                    break;
                }
            }
        }


        private int EncontrarMejorAjuste(int particionesRequeridas)
        {
            int indiceMejorAjuste = -1;
            int espacioMinimo = int.MaxValue; // Inicializamos el espacio mínimo como un valor muy alto

            for (int i = 0; i < pilaProcesos.Length; i++)
            {
                if (pilaProcesos[i] == null) // Si la partición está vacía
                {
                    int espacioContinuo = 0;

                    // Buscar espacio contiguo
                    while (i + espacioContinuo < pilaProcesos.Length && pilaProcesos[i + espacioContinuo] == null && espacioContinuo < particionesRequeridas)
                    {
                        espacioContinuo++;
                    }

                    if (espacioContinuo == particionesRequeridas)
                    {
                        // Si encontramos suficiente espacio continuo
                        if (espacioContinuo < espacioMinimo)
                        {
                            espacioMinimo = espacioContinuo;
                            indiceMejorAjuste = i;
                        }
                    }
                }
            }

            return indiceMejorAjuste; // Devuelve el índice del mejor ajuste, o -1 si no hay suficiente espacio
        }

        //PEOR AJUSTE
        private void AsignarProcesosAPilaPA()
        {
            while (colaProcesos.Count > 0)
            {
                var proceso = colaProcesos.Peek();
                int particionesRequeridas = proceso.Tamano / 10;

                // Encontrar el bloque de memoria más grande disponible
                int indicePeorAjuste = EncontrarPeorAjuste(particionesRequeridas);

                if (indicePeorAjuste != -1)
                {
                    // Asignar el proceso a las particiones disponibles
                    for (int i = 0; i < particionesRequeridas; i++)
                    {
                        pilaProcesos[indicePeorAjuste + i] = proceso;
                        // Aquí puedes actualizar los labels de la pila para reflejar el proceso asignado
                        //pilaLabels[indicePeorAjuste + i].BackColor = Color.Red;
                        //pilaLabels[indicePeorAjuste + i].Text = $"{proceso.Nombre} - {proceso.Duracion} segs\n";
                    }

                    colaProcesos.Dequeue();
                    ActualizarCola();
                    ActualizarMemoria();
                }
                else
                {
                    // Si no hay espacio suficiente, salir del bucle
                    break;
                }
            }
        }


        private int EncontrarPeorAjuste(int particionesRequeridas)
        {
            int indicePeorAjuste = -1;
            int espacioMaximo = 0; // Inicializamos el espacio máximo como 0

            for (int i = 0; i < pilaProcesos.Length; i++)
            {
                if (pilaProcesos[i] == null) // Si la partición está vacía
                {
                    int espacioContinuo = 0;

                    // Buscar espacio contiguo
                    while (i + espacioContinuo < pilaProcesos.Length && pilaProcesos[i + espacioContinuo] == null)
                    {
                        espacioContinuo++;
                    }

                    if (espacioContinuo >= particionesRequeridas && espacioContinuo > espacioMaximo)
                    {
                        espacioMaximo = espacioContinuo;
                        indicePeorAjuste = i;
                    }
                }
            }

            return indicePeorAjuste; // Devuelve el índice del peor ajuste, o -1 si no hay suficiente espacio
        }

        //MEMORIA,CLICKS,ETC...
        private void ActualizarCola()
        {
            for (int i = 0; i < colaLabels.Length; i++)
            {
                if (i < colaProcesos.Count)
                {
                    var proceso = colaProcesos.ToArray()[i];
                    colaLabels[i].Text = $"{proceso.Nombre}\n{proceso.Duracion}s\n{proceso.Tamano} MBs";
                }
                else
                {
                    colaLabels[i].Text = "Vacío";
                }
            }
        }


        private void ActualizarMemoria()
        {
            int memoriaDisponible = 0;
            int memoriaUsada = MemoriaUtilizada;
            lblMemoriaUtilizada.Text = $"MEMORIA UTILIZADA: {MemoriaUtilizada} MBs";
            memoriaDisponible = 500 - memoriaUsada;
            lblMemoriaDisponible.Text = $"MEMORIA DISPONIBLE: {memoriaDisponible} MBs";
        }

        private void PilaLabel_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            int index = Array.IndexOf(pilaLabels, clickedLabel);

            if (index != -1 && pilaProcesos[index] != null)
            {
                var proceso = pilaProcesos[index];

                if (proceso.EnPausa)
                {
                    // Si el proceso está en pausa, reanudarlo
                    clickedLabel.BackColor = Color.Red; // Cambiar a color rojo
                    proceso.EnPausa = false; // Reanudar
                }
                else
                {
                    // Si el proceso está en ejecución, pausarlo
                    clickedLabel.BackColor = Color.Yellow; // Cambiar a color amarillo
                    proceso.EnPausa = true; // Pausar
                }

                // Iniciar la reducción continua de tiempo si el simulador está detenido y el proceso no está pausado
                if (!simuladorActivo && !proceso.EnPausa)
                {
                    // Aquí empezamos la reducción continua del tiempo
                    ReducirTiempoContinuamente(index);
                }
            }
        }


        private void ReducirTiempoContinuamente(int index)
        {
            // Usamos un temporizador para disminuir el tiempo cada segundo
            System.Windows.Forms.Timer tiempoReducir = new System.Windows.Forms.Timer();
            tiempoReducir.Interval = 1000; // Reducir cada 1 segundo
            tiempoReducir.Tick += (sender, e) =>
            {
                if (pilaProcesos[index] != null)
                {
                    var proceso = pilaProcesos[index];

                    if (proceso.Duracion > 0)
                    {
                        // Reducir el tiempo del proceso
                        proceso.Duracion--;
                        pilaLabels[index].Text = $"{proceso.Nombre} - {proceso.Duracion} segs\n";
                        ActualizarMemoria(); // Actualizar estado de la memoria
                    }

                    // Si el proceso termina, detener el temporizador y liberar la memoria
                    if (proceso.Duracion <= 0)
                    {
                        tiempoReducir.Stop(); // Detener el temporizador
                        pilaProcesos[index] = null;
                        pilaLabels[index].Text = "10 MB"; // Restablecer el estado de la partición
                        pilaLabels[index].BackColor = Color.White;
                        pilaLabels[index].Size = new System.Drawing.Size(200, 18);
                        ActualizarMemoria(); // Actualizar el estado de la memoria
                    }
                }
            };
            tiempoReducir.Start(); // Iniciar el temporizador
        }

        private void PrimerAjuste_Click(object sender, EventArgs e)
        {
            ajusteSeleccionado = "Primer Ajuste"; // Asignar la opción seleccionada
        }

        private void MejorAjuste_Click(object sender, EventArgs e)
        {
            ajusteSeleccionado = "Mejor Ajuste"; // Asignar la opción seleccionada
        }

        private void PeorAjuste_Click(object sender, EventArgs e)
        {
            ajusteSeleccionado = "Peor Ajuste"; // Asignar la opción seleccionada
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            string idEliminar = txtIdEliminar.Text.Trim(); // Obtener el ID ingresado por el usuario

            if (string.IsNullOrEmpty(idEliminar))
            {
                MessageBox.Show("Por favor, ingrese un ID válido.");
                return;
            }

            // Convertir el ID a entero y buscar el proceso en el diccionario
            if (int.TryParse(idEliminar, out int idNumerico) && diccionarioProcesos.ContainsKey(idNumerico))
            {
                Proceso procesoEliminar = diccionarioProcesos[idNumerico];

                // Eliminar el proceso de la cola
                colaProcesos = new Queue<Proceso>(colaProcesos.Where(p => p != procesoEliminar));

                // Liberar la memoria y las particiones ocupadas por el proceso
                int particionesRequeridas = procesoEliminar.Tamano / 10;

                // Identificar las particiones que ocupó el proceso
                for (int i = 0; i < pilaProcesos.Length; i++)
                {
                    if (pilaProcesos[i] == procesoEliminar)
                    {
                        // Liberar las particiones en la pila
                        for (int j = i; j < i + particionesRequeridas; j++)
                        {
                            if (j < pilaProcesos.Length && pilaProcesos[j] == procesoEliminar)
                            {
                                pilaProcesos[j] = null;
                                pilaLabels[j].Text = "10 MB";
                                pilaLabels[j].BackColor = Color.White;
                                pilaLabels[j].Size = new System.Drawing.Size(200, 18);
                            }
                        }
                        break;
                    }
                }

                // Actualizar la memoria
                ActualizarMemoria();

                // Mostrar un mensaje de éxito
                MessageBox.Show($"Proceso {idNumerico} eliminado correctamente.");

                // Eliminar el proceso del diccionario
                diccionarioProcesos.Remove(idNumerico);
            }
            else
            {
                MessageBox.Show($"No se encontró un proceso con el ID {idEliminar}.");
            }
        }


        public class Proceso
        {
            public string Nombre { get; set; }
            public int Duracion { get; set; }
            public int Tamano { get; set; }
            public bool EnPausa { get; set; }
            public bool isPaused { get; set; }  // Nuevo atributo
            public int Id { get; set; }
            public int Peso { get; set; }
            private static int contadorId = 1;  // Contador estático para generar IDs únicos

            public Proceso(string nombre, int duracion, int tamano)
            {
                Id = contadorId++;  // Asigna un ID único y luego incrementa el contador
                Nombre = nombre;
                Duracion = duracion;
                Tamano = tamano;
                EnPausa = false;
            }
        }

        private void lblMemoriaTotal_Click(object sender, EventArgs e)
        {

        }
    }
}