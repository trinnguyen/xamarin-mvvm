using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Mvvm
{
	public class BindableBase : INotifyPropertyChanged
	{
		/// <summary>
		/// Fired on property changed
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Set value to property, fire event Changed if success
		/// </summary>
		/// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
		/// <param name="storage">Storage.</param>
		/// <param name="value">Value.</param>
		/// <param name="propertyName">Property name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (object.Equals(storage, value)) 
				return false;

			storage = value;
			this.RaisePropertyChanged(propertyName);

			return true;
		}

		/// <summary>
		/// Notify to fire property changed
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));	
			}
		}

		/// <summary>
		/// Notify to fire property changed with Lamba Expression
		/// </summary>
		/// <param name="propertyExpression">Property expression.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
			this.RaisePropertyChanged(propertyName);
		}

		public virtual string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
		{ 
			var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
			return propertyName;
		}
	}
}

