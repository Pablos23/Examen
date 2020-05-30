using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Prueba.Behaviors
{
    public class EmailBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty OkProperty = BindableProperty.Create("Ok", typeof(bool),
                                         typeof(EmailBehavior), false);
               
        public bool OK {
            get => (bool)GetValue(OkProperty);
            set => SetValue(OkProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }
        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry caja = sender as Entry;
            String mail = caja.Text;
            int ultimopunto = mail.LastIndexOf(".");
            String dominio = mail.Substring(ultimopunto + 1);
            if (caja != null)
            {
                try
                {
                    if (mail.Contains("@") == false)
                    {
                        caja.TextColor = Color.Red;
                        OK = false;
                    }
                    else if (mail.StartsWith("@") || mail.EndsWith("@"))
                    {
                        caja.TextColor = Color.Red;
                        OK = false;
                    }
                    else if (mail.IndexOf("@") != mail.LastIndexOf("@"))
                    {
                        caja.TextColor = Color.Red;
                        OK = false;
                    }
                    else if (mail.IndexOf(".") == -1)
                    {
                        caja.TextColor = Color.Red;
                        OK = false;
                    }
                    else if (mail.LastIndexOf(".") < mail.IndexOf("@"))
                    {
                        caja.TextColor = Color.Red;
                        OK = false;
                    }
                    else if (dominio.Length <= 2 || dominio.Length >= 4)
                    {
                        caja.TextColor = Color.Red;
                        OK = false;
                    }
                    else
                    {
                        caja.TextColor = Color.Green;
                        OK = true;
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
