using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using WarehouseOrganizer.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace WarehouseOrganizer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool _isReadOnly = false;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); OnPropertyChanged("isReady"); }
        }

        public bool isReady { get { return !IsBusy; } }

        string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }


        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region infoMessage
        protected void ShowErrorUserMessage(BaseViewModel viewModel, String message, String cancelBtnText)
        {
            InfoUserMessage messageInfo = new InfoUserMessage() { Title = "Error", MessageText = message, ButtonCancelText = cancelBtnText };
            MessagingCenter.Send<BaseViewModel, InfoUserMessage>(viewModel, MessageConst.InfoUserMessage, messageInfo);
        }

        protected void ShowInformationUserMessage(BaseViewModel viewModel, String message, String cancelBtnText)
        {
            InfoUserMessage messageInfo = new InfoUserMessage() { Title = "Information", MessageText = message, ButtonCancelText = cancelBtnText };
            MessagingCenter.Send<BaseViewModel, InfoUserMessage>(viewModel, MessageConst.InfoUserMessage, messageInfo);
        }

        protected void ShowInfoUserMessage(BaseViewModel viewModel, String title, String message, String cancelBtnText)
        {
            InfoUserMessage messageInfo = new InfoUserMessage() { Title = title, MessageText = message, ButtonCancelText = cancelBtnText };
            MessagingCenter.Send<BaseViewModel, InfoUserMessage>(viewModel, MessageConst.InfoUserMessage, messageInfo);
        }

        protected void ShowInfoUserMessage(BaseViewModel viewModel, InfoUserMessage message)
        {
            MessagingCenter.Send<BaseViewModel, InfoUserMessage>(viewModel, MessageConst.InfoUserMessage, message);
        }
        #endregion

        #region Action User Message
        
        protected void ShowActionUserMessage(BaseViewModel viewModel, String title, String message, String cancelBtnText, String okBtnText, Action<bool> callBack)
        {
            ActionUserMessage actionMessage = new ActionUserMessage() { Title = title, MessageText = message, ButtonCancelText = cancelBtnText, ButtonOkText = okBtnText, CallBack = callBack };
            MessagingCenter.Send<BaseViewModel, ActionUserMessage>(viewModel, MessageConst.ActionUserMessage, actionMessage);
        }

        protected void ShowActionUserMessage(BaseViewModel viewModel, ActionUserMessage message)
        {
            MessagingCenter.Send<BaseViewModel, ActionUserMessage>(viewModel, MessageConst.ActionUserMessage, message);
        }

        #endregion
    }
}
