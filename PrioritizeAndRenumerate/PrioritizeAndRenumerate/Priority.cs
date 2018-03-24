using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritizeAndRenumerate
{
    class Priority : INotifyPropertyChanged
    {
        private int _priorityID;
        private string _details;

        public int PriorityID
        {
            get
            {
                return this._priorityID;
            }
            set
            {
                _priorityID = value;
                OnPropertyChanged("PriorityList");
            }
        }
        public string Details
        {
            get
            {
                return this._details;
            }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
        }

        public bool Criteria { get; set; }

        public string ConditionalSelection { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        public Priority(int id, string details, bool criteria, string conditionalSelection)
        {
            PriorityID = id;
            Details = details;
            Criteria = criteria;
            ConditionalSelection = conditionalSelection;
        }

        public Priority(int id, string details, bool criteria)
        {
            PriorityID = id;
            Details = details;
            Criteria = criteria;
        }



        public static ObservableCollection<Priority> GetSamplePriorities()
        {
            ObservableCollection<Priority> priorities = new ObservableCollection<Priority>();
            Priority priority;

            priority = new Priority(1, "DetailSample1", true);
            priorities.Add(priority);

            priority = new Priority(2, "DetailSample2", true);
            priorities.Add(priority);

            priority = new Priority(3, "DetailSample3", true);
            priorities.Add(priority);

            priority = new Priority(4, "DetailSample4", true);
            priorities.Add(priority);

            priority = new Priority(5, "DetailSample5", true);
            priorities.Add(priority);

            priority = new Priority(6, "DetailSample6", true);
            priorities.Add(priority);

            priority = new Priority(7, "DetailSample7", true);
            priorities.Add(priority);

            priority = new Priority(8, "DetailSample8", true);
            priorities.Add(priority);

            return priorities;
        }

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

    }
}
