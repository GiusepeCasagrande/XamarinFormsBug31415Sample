using System;
namespace XamarinFormsBug31415Sample.Utils
{
    public interface IUserDialogsService
    {
        /// <summary>
        /// Shows the error Dialog.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        void ShowError(string message = "");

        /// <summary>
        /// Shows the succes Dialog.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        void ShowSucces(string message = "");

        /// <summary>
        /// Shows the loading Dialog.
        /// </summary>
        /// <param name="title">Loading message title.</param>
        void ShowLoading(string title = "");

        /// <summary>
        /// Hides the loading.
        /// </summary>
        void HideLoading();
    }
}
