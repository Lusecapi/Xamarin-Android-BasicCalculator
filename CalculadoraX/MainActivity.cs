using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace CalculadoraX
{
    [Activity(Label = "CalculadoraX", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        EditText pantalla;
        //Botones de acciones
        Button botonClear, botonClearAll, botonBorrar, botonCopiar;
        //botones numericos
        Button botonSigno, botonPunto, boton0, boton1, boton2, boton3, boton4, boton5, boton6, boton7, boton8, boton9;
        //botones de operaciones
        Button botonSuma, botonResta, botonMult, botonDiv, botonIgual;


        //Emun con tipos de operaciones
        private enum Operacion
        {
            Suma,
            Resta,
            Multiplicacion,
            Division,
        }

        //Variables de la calculadora
        string numeroDigitado = "0";
        bool numeroTienePunto;
        double numeroA = 0;
        Operacion operacion;
        bool estaOperando;
        double numeroB = 0;
        double resultado = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            pantalla = FindViewById<EditText>(Resource.Id.editText1);
            botonCopiar = FindViewById<Button>(Resource.Id.button1);
            botonClearAll = FindViewById<Button>(Resource.Id.button2);
            botonClear = FindViewById<Button>(Resource.Id.button3);
            botonBorrar = FindViewById<Button>(Resource.Id.button4);
            boton7 = FindViewById<Button>(Resource.Id.button5);
            boton8 = FindViewById<Button>(Resource.Id.button6);
            boton9 = FindViewById<Button>(Resource.Id.button7);
            boton4 = FindViewById<Button>(Resource.Id.button8);
            boton5 = FindViewById<Button>(Resource.Id.button9);
            boton6 = FindViewById<Button>(Resource.Id.button10);
            boton1 = FindViewById<Button>(Resource.Id.button11);
            boton2 = FindViewById<Button>(Resource.Id.button12);
            boton3 = FindViewById<Button>(Resource.Id.button13);
            boton0 = FindViewById<Button>(Resource.Id.button14);
            botonPunto = FindViewById<Button>(Resource.Id.button15);
            botonSigno = FindViewById<Button>(Resource.Id.button16);
            botonSuma = FindViewById<Button>(Resource.Id.button17);
            botonResta = FindViewById<Button>(Resource.Id.button18);
            botonMult = FindViewById<Button>(Resource.Id.button19);
            botonDiv = FindViewById<Button>(Resource.Id.button20);
            botonIgual = FindViewById<Button>(Resource.Id.button21);

            botonCopiar.Click += BotonCopiar_Click;
            botonClearAll.Click += BotonClearAll_Click;
            botonClear.Click += BotonClear_Click;
            botonBorrar.Click += BotonBorrar_Click;

            boton7.Click += Boton7_Click;
            boton8.Click += Boton8_Click;
            boton9.Click += Boton9_Click;
            boton4.Click += Boton4_Click;
            boton5.Click += Boton5_Click;
            boton6.Click += Boton6_Click;
            boton1.Click += Boton1_Click;
            boton2.Click += Boton2_Click;
            boton3.Click += Boton3_Click;
            boton0.Click += Boton0_Click;
            botonPunto.Click += BotonPunto_Click;
            botonSigno.Click += BotonSigno_Click;

            botonSuma.Click += BotonSuma_Click;
            botonResta.Click += BotonResta_Click;
            botonMult.Click += BotonMult_Click;
            botonDiv.Click += BotonDiv_Click;
            botonIgual.Click += BotonIgual_Click;

            mostrarEnPantalla(numeroDigitado);
        }

        #region Eventos botones operaciones

        private void BotonIgual_Click(object sender, System.EventArgs e)
        {
            if (estaOperando)
            {
                estaOperando = false;
                double aux;
                if (double.TryParse(numeroDigitado, out aux))
                {
                    numeroB = aux;
                }
                else
                {
                    numeroB = long.Parse(numeroDigitado);
                }
                resultado = operar(numeroA, numeroB);
                numeroDigitado = resultado.ToString();
                mostrarEnPantalla(numeroDigitado);
            }
        }

        private void BotonDiv_Click(object sender, System.EventArgs e)
        {
            setOperacion(Operacion.Division);
        }

        private void BotonMult_Click(object sender, System.EventArgs e)
        {
            setOperacion(Operacion.Multiplicacion);
        }

        private void BotonResta_Click(object sender, System.EventArgs e)
        {
            setOperacion(Operacion.Resta);
        }

        private void BotonSuma_Click(object sender, System.EventArgs e)
        {
            setOperacion(Operacion.Suma);
        }

        #endregion

        #region Eventos botones Numericos

        private void BotonSigno_Click(object sender, System.EventArgs e)
        {
            var numero = double.Parse(numeroDigitado);
            numeroDigitado = (numero * -1).ToString();
            mostrarEnPantalla(numeroDigitado);
        }

        private void BotonPunto_Click(object sender, System.EventArgs e)
        {
            if (!numeroTienePunto)
            {
                numeroTienePunto = true;
                digitar(".");
            }
        }

        private void Boton0_Click(object sender, System.EventArgs e)
        {
            digitar("0");
        }

        private void Boton3_Click(object sender, System.EventArgs e)
        {
            digitar("3");
        }

        private void Boton2_Click(object sender, System.EventArgs e)
        {
            digitar("2");
        }

        private void Boton1_Click(object sender, System.EventArgs e)
        {
            digitar("1");
        }

        private void Boton6_Click(object sender, System.EventArgs e)
        {
            digitar("6");
        }

        private void Boton5_Click(object sender, System.EventArgs e)
        {
            digitar("5");
        }

        private void Boton4_Click(object sender, System.EventArgs e)
        {
            digitar("4");
        }

        private void Boton9_Click(object sender, System.EventArgs e)
        {
            digitar("9");
        }

        private void Boton8_Click(object sender, System.EventArgs e)
        {
            digitar("8");
        }

        private void Boton7_Click(object sender, System.EventArgs e)
        {
            digitar("7");
        }

        #endregion

        #region Eventos botones Acciones

        private void BotonBorrar_Click(object sender, System.EventArgs e)
        {
            if (numeroDigitado.Length > 1)
            {
                if (numeroDigitado.Substring(numeroDigitado.Length - 1) == ".")
                {
                    numeroTienePunto = false;
                }
                numeroDigitado = numeroDigitado.Substring(0, numeroDigitado.Length - 1);
            }
            else
            {
                numeroDigitado = "0";
            }
            mostrarEnPantalla(numeroDigitado);
        }

        private void BotonClear_Click(object sender, System.EventArgs e)
        {
            numeroDigitado = "0";
            mostrarEnPantalla(numeroDigitado);
        }

        private void BotonClearAll_Click(object sender, System.EventArgs e)
        {
            numeroDigitado = "0";
            numeroA = 0;
            numeroB = 0;
            estaOperando = false;
            resultado = 0;
            mostrarEnPantalla(numeroDigitado);
        }

        private void BotonCopiar_Click(object sender, System.EventArgs e)
        {
            var clipboard = (ClipboardManager)GetSystemService(ClipboardService);
            var clip = ClipData.NewPlainText("Resultado", resultado.ToString());
            clipboard.PrimaryClip = clip;
            Toast.MakeText(this, "Resultado copiado en el Clipboard", ToastLength.Short).Show();
        }

        #endregion

        #region Metodos y Funciones

        private void digitar(string digito)
        {
            if (numeroDigitado == "0" && digito != ".")//Evitar que ponga ceros a la izquierda
            {
                numeroDigitado = digito;
            }
            else
            {
                numeroDigitado += digito;
            }

            mostrarEnPantalla(numeroDigitado);
        }

        private void mostrarEnPantalla(string texto)
        {
            pantalla.Text = texto;
        }

        /// <summary>
        /// Para ahorrar lineas de codigo
        /// </summary>
        /// <param name="operacionPresionada"></param>
        private void setOperacion(Operacion operacionPresionada)
        {
            if (!estaOperando)
            {
                estaOperando = true;
                operacion = operacionPresionada;
                double aux;
                if (double.TryParse(numeroDigitado, out aux))
                {
                    numeroA = aux;
                }
                else
                {
                    numeroA = long.Parse(numeroDigitado);
                }
                numeroDigitado = "0";
                numeroTienePunto = false;
                mostrarEnPantalla(numeroDigitado);
            }
            else
            {
                double aux;
                if (double.TryParse(numeroDigitado, out aux))
                {
                    numeroB = aux;
                }
                else
                {
                    numeroB = long.Parse(numeroDigitado);
                }
                resultado = operar(numeroA, numeroB);
                mostrarEnPantalla(resultado.ToString());
                numeroA = resultado;
                numeroDigitado = "0";
                estaOperando = true;
                operacion = operacionPresionada;
            }
        }

        private double operar(double num1, double num2)
        {
            if (operacion == Operacion.Suma)
            {
                return num1 + num2;
            }
            else
                if (operacion == Operacion.Resta)
            {
                return num1 - num2;
            }
            else
                if (operacion == Operacion.Multiplicacion)
            {
                return num1 * num2;
            }
            else
                if (operacion == Operacion.Division)
            {
                return num1 / num2;
            }
            else
            {
                Toast.MakeText(this, "Error en la Operación", ToastLength.Short).Show();
                return 0;
            }
        }

        #endregion
    }
}

