using System;
using System.Collections.Generic;
using System.Windows;

namespace ViewModels.DialogServices
{ 
    // The ViewModels are registered to the views.
    public class DialogService : IDialogService
    {
        private readonly Window owner;      

        public IDictionary<Type, Type> Mappings { get; }
        public DialogService(Window owner)
        {
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }
        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            Mappings.Add(typeof(TViewModel), typeof(TView));
        }
        
        
        // Shows a dialog box with the View that corresponds the viewmodel that gets passed
        // as an argument.
        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            void handler(object sender, DialogCloseRequestedEventArgs e)
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            }

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            return dialog.ShowDialog();
        }
    }
}
