using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Anapher.Wpf.Toolkit.Utilities
{
    /// <summary>
    ///     An observable collection that mapps it's items to the item's of another observable collection (keeps them in sync)
    /// </summary>
    /// <typeparam name="TViewModel">The view model (new object that mapps to a source object)</typeparam>
    /// <typeparam name="TModel">The model (source object)</typeparam>
    public class MappedObservableCollection<TViewModel, TModel> : ObservableCollection<TViewModel>
    {
        private readonly ObservableCollection<TModel> _sourceCollection;
        private readonly Func<TModel, TViewModel> _viewModelFactory;
        private readonly Dictionary<TModel, TViewModel> _viewModels = new Dictionary<TModel, TViewModel>();

        /// <summary>
        ///     Initialize a new instance of <see cref="MappedObservableCollection{TViewModel,TModel}"/> using a factory
        /// </summary>
        /// <param name="sourceCollection">The source collection</param>
        /// <param name="viewModelFactory">The factory that creates a <see cref="TViewModel"/> from a <see cref="TModel"/></param>
        public MappedObservableCollection(ObservableCollection<TModel> sourceCollection, Func<TModel, TViewModel> viewModelFactory)
        {
            _sourceCollection = sourceCollection;
            _viewModelFactory = viewModelFactory;

            foreach (var model in sourceCollection)
                Add(CreateViewModel(model));

            sourceCollection.CollectionChanged += SourceCollectionOnCollectionChanged;
        }

        private TViewModel CreateViewModel(TModel model)
        {
            var viewModel = _viewModelFactory(model);
            _viewModels[model] = viewModel;
            return viewModel;
        }

        private void SourceCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    foreach (var model in e.NewItems.Cast<TModel>()) Add(CreateViewModel(model));

                    foreach (var model in e.OldItems.Cast<TModel>())
                    {
                        var viewModel = _viewModels[model];
                        _viewModels.Remove(model);

                        Remove(viewModel);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Clear();

                    foreach (var model in _sourceCollection)
                        Add(CreateViewModel(model));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}