using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

//TODO add blank rows and have an add button on the next consequtive row

namespace PrioritizeAndRenumerate
{
    class SimpleVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Priority _priorityToMove;

        public bool testCondition3 {get; set;}

        public ICommand SelectionChanged { get; set; }
        public ICommand SelectionChanged2 { get; set; }
        public ICommand DataLoadedCmd { get; set; }
        public ICommand DetailsSelectionChanged { get; set; }
        public ICommand XButtonClick { get; set; }
        //May need to do like an ON property changed property?

        bool _collectionChanging;

        //private void RaiseCanExecuteChanged()
        //{
        //    DelegateCommand<object> command = LoginCommand as DelegateCommand<object>;
        //    command.RaiseCanExecuteChanged();
        //}

        public SimpleVM()
        {
            SelectionChanged = new DelegateCommand(OnSelectionChangedExecute, SelectionChangedCommand_CanExecute);
            DetailsSelectionChanged = new DelegateCommand(OnDetailsSelectionChangedExecute, SelectionChangedCommand_CanExecute);
            DataLoadedCmd = new DelegateCommand(DataLoadedExecute, SelectionChangedCommand_CanExecute);
            XButtonClick = new DelegateCommand(AddTestCondition, AlwaysExecute);
            //object SaveCommand_Execute = null;
            //SelectionChanged2 = new DelegateCommand<object>(GetSaveCommand_Execute(SaveCommand_Execute), SaveCommand_CanExecute);
            //SelectionChanged3 = new DelegateCommand()
            //LoadSampleDetails();

        }

        private bool AlwaysExecute(object obj)
        {
            return true;
        }

        private void AddTestCondition(object obj)
        {
            TestCondition1.Add("testCondition1");
            TestCondition1.Add("testCondition2");
        }

        private void AddTestCondition2()
        {
            TestCondition1.Add("testCondition1a");
            TestCondition1.Add("testCondition2a");
        }

        private void DataLoadedExecute(object obj)
        {
            RoutedEventArgs rea = obj as RoutedEventArgs;
            RadGridView rgv1 = rea.Source as RadGridView;
            rgv1.SelectedItem = ((ObservableCollection<Priority>)rgv1.ItemsSource).Last();
            //this.radGridView.SelectedItem = ((ObservableCollection<Employee>)this.radGridView.ItemsSource).First();
            //rgv1.SelectedItem = rgv1.Items[_priorityList.Count() - 2];

        }

        public bool SelectionChangedCommand_CanExecute(object obj)
        {
            
            //var prioritiesCollection = obj as SelectionChangedEventArgs;
            //var oldVal = prioritiesCollection.RemovedItems;
            //if (oldVal.Count > 0 && _collectionChanging != true) return true;
            //return false;
            return true;
        }


        private void OnSelectionChangedExecute(object obj)
        {
            if (_collectionChanging == true) return;
            var prioritiesCollection = obj as SelectionChangedEventArgs;
            //SelectionChanged = null;
            var oldVal = prioritiesCollection.RemovedItems;
            if (oldVal.Count <= 0) return;
            int oldInt = Convert.ToInt32(oldVal[0]);
            var newVal = prioritiesCollection.AddedItems;
            int newInt = Convert.ToInt32(newVal[0]);
            Renumerate(newInt, oldInt);

            ComboBox cb1 = obj as ComboBox;
            var item = obj as ComboBoxItem;
            var m = prioritiesCollection.Source as RadComboBox;
            var n1 = cb1.ParentOfType<RadGridView>();

            RadGridView rgv1 = m.Parent as RadGridView;
            //rgv1.SelectedItem = rgv1.Items[_priorityList.Count() - 2];
        }

        private void OnDetailsSelectionChangedExecute(object obj)
        {
            if (_collectionChanging == true) return;
            var prioritiesCollection = obj as SelectionChangedEventArgs;
            //SelectionChanged = null;
            var oldVal = prioritiesCollection.RemovedItems;
            if (oldVal.Count <= 0) return;
            //int oldInt = Convert.ToInt32(oldVal[0]);
            //var newVal = prioritiesCollection.AddedItems;
            //int newInt = Convert.ToInt32(newVal[0]);
            //Renumerate(newInt, oldInt);
            MessageBox.Show(oldVal.ToString());
        }

        private void Renumerate2(int newInt, int oldInt)
        {
            ObservableCollection<Priority> localList = new ObservableCollection<Priority>();
            if (newInt == oldInt) return;
            if (newInt > oldInt)
            {
                int diff = newInt - oldInt;
                for (int i = 0; i < diff; i--)
                {

                    PriorityList[i].PriorityID++;

                    //1 --> 3   1.2
                    //2         2.3
                    //3         3.1

                }
            }
        }

        private void Renumerate(int newInt, int oldInt)
        {
            _collectionChanging = true;
            bool firstPass = true;
            ObservableCollection<Priority> localList = new ObservableCollection<Priority>();
            if (newInt == oldInt) return;
            if (newInt > oldInt)
            {
                //1 --> 3
                //2
                //3
                foreach (Priority pModel in PriorityList)
                {
                    if (pModel.PriorityID == newInt && firstPass)
                    {
                        firstPass = false;
                        localList.Add(pModel);
                        continue;
                    }
                    if (pModel.PriorityID > oldInt && pModel.PriorityID <= newInt && !firstPass)
                    {
                        pModel.PriorityID --;
                        localList.Add(pModel);
                    }
                    //1         1.3
                    //2         2.1
                    //3 --> 1   3.2
                }
            }
            if (newInt < oldInt)
            {
            int diff = oldInt - newInt;
                for (int i = 1; i <= diff + 1; i++)
                {
                    PriorityList[oldInt - i].PriorityID++;
                }
                PriorityList[newInt].PriorityID = oldInt;


                foreach (Priority pModel in PriorityList)
                {
                    //if ()


                    //if (pModel.PriorityID == newInt && firstPass)
                    //{
                    //    firstPass = false;
                    //    pModel.PriorityID++;
                    //    localList.Add(pModel);
                    //    continue;
                    //}
                    //if (pModel.PriorityID > newInt)
                    //{
                    //    pModel.PriorityID++;
                    //    localList.Add(pModel);
                    //    continue;
                    //}
                    //if (pModel.PriorityID == newInt)
                    //{
                    //    pModel.PriorityID = newInt;
                    //    localList.Add(pModel);
                    //}
                }
            }

            ObservableCollection<Priority> prioritySort = new ObservableCollection<Priority>(
            _priorityList.OrderBy(Priority => Priority.PriorityID));
            PriorityList.Clear();
            foreach (Priority pri in prioritySort)
                PriorityList.Add(pri);
            _collectionChanging = true;


        }

        private ObservableCollection<Priority> _priorityList;

        public ObservableCollection<Priority> PriorityList
        {
            get
            {
                if (this._priorityList == null)
                {
                    this._priorityList = Priority.GetSamplePriorities();
                }
                return this._priorityList;
            }
            set
            {
                _priorityList = value;
                OnPropertyChanged("PriorityList");
            }
        }

        private ObservableCollection<int> _priorityIDs;
        private Priority cbChangedModel;

        public ObservableCollection<int> PriorityIDs
        {
            get
            {
                if (this._priorityIDs == null)
                {
                    this._priorityIDs = GetIds(_priorityList);
                }
                return this._priorityIDs;
            }
            set { _priorityIDs = value;
                OnPropertyChanged("PriorityIDs");
            }
        }

        private ObservableCollection<string> _detailsList;
        public ObservableCollection<string> DetailsList
        {
            get
            {
                if (this._detailsList == null)
                {
                    this._detailsList = LoadSampleDetails(_priorityList);
                }
                return this._detailsList;
            }
            set
            {
                _detailsList = value;
                OnPropertyChanged("DetailsList");
            }
        }

        public ObservableCollection<string> _testCondition1;
        public ObservableCollection<string> TestCondition1
        {
            get
            {
                if (_testCondition1 == null) _testCondition1 = new ObservableCollection<string>();

                return _testCondition1;
            }
            set
            {
                _testCondition1 = value;
                OnPropertyChanged("TestCondition1");
            }
        }


        public object SaveCommand_CanExecute { get; private set; }

        public static ObservableCollection<int> GetIds(ObservableCollection<Priority> list)
        {
            int i = 1;
            ObservableCollection<int> priorityList = new ObservableCollection<int>();
            foreach (var listItem in list)
            {
                priorityList.Add(i);
                i++;
            }
            return priorityList;
        }

        private static ObservableCollection<string> LoadSampleDetails(ObservableCollection<Priority> list)
        {
            int i = 1;
            ObservableCollection<string> priorityList = new ObservableCollection<string>();
            foreach (var listItem in list)
            {
                priorityList.Add($"DetailSample{i.ToString()}");
                i++;
            }
            return priorityList;
        }

        #region INotifyPropertyChanged
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
