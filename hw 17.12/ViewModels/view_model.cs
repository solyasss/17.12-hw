using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using hw.Models;
using hw.Commands;

namespace hw.ViewModels
{
    public class ViewModels : INotifyPropertyChanged
    {
        private byte color_a;
        private byte color_r;
        private byte color_g;
        private byte color_b;

        private bool _a_enabled = true;
        private bool _r_enabled = true;
        private bool _g_enabled = true;
        private bool _b_enabled = true;

        private color_model color_model;

        public byte a
        {
            get { return color_a; }
            set
            {
                if (color_a != value)
                {
                    color_a = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public byte r
        {
            get { return color_r; }
            set
            {
                if (color_r != value)
                {
                    color_r = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public byte g
        {
            get { return color_g; }
            set
            {
                if (color_g != value)
                {
                    color_g = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public byte b
        {
            get { return color_b; }
            set
            {
                if (color_b != value)
                {
                    color_b = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public bool a_enabled
        {
            get { return _a_enabled; }
            set
            {
                if (_a_enabled != value)
                {
                    _a_enabled = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public bool r_enabled
        {
            get { return _r_enabled; }
            set
            {
                if (_r_enabled != value)
                {
                    _r_enabled = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public bool g_enabled
        {
            get { return _g_enabled; }
            set
            {
                if (_g_enabled != value)
                {
                    _g_enabled = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        public bool b_enabled
        {
            get { return _b_enabled; }
            set
            {
                if (_b_enabled != value)
                {
                    _b_enabled = value;
                    OnPropertyChanged();
                    update_color();
                }
            }
        }

        private void update_color()
        {
            var color = new color_model
            {
                A = a_enabled ? a : (byte)255,
                R = r_enabled ? r : (byte)0,
                G = g_enabled ? g : (byte)0,
                B = b_enabled ? b : (byte)0
            };
            _current_color = new SolidColorBrush(color.ToColor());
            OnPropertyChanged("current_color");
            OnPropertyChanged("color_display");
            
            add_command.RaiseCanExecuteChanged();
        }

        private SolidColorBrush _current_color = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

        public SolidColorBrush current_color
        {
            get { return _current_color; }
            set
            {
                if (_current_color != value)
                {
                    _current_color = value;
                    OnPropertyChanged();
                }
            }
        }

        public string color_display
        {
            get
            {
                return $"A:{(a_enabled ? a : 255)}, R:{(r_enabled ? r : 0)}, G:{(g_enabled ? g : 0)}, B:{(b_enabled ? b : 0)}";
            }
        }

        public ObservableCollection<color_model> colors { get; set; }

        public RelayCommand add_command { get; set; }
        public RelayCommand delete_command { get; set; }

        public color_model selected_color_model
        {
            get { return color_model; }
            set
            {
                if (color_model != value)
                {
                    color_model = value;
                    OnPropertyChanged();
                    delete_command.RaiseCanExecuteChanged();
                }
            }
        }

        public ViewModels()
        {
            colors = new ObservableCollection<color_model>();
            add_command = new RelayCommand(add_color, op_add_color);
            delete_command = new RelayCommand(delete_color, op_delete_color);
            update_color();
        }

        private void add_color()
        {
            var new_color = new color_model
            {
                A = a_enabled ? a : (byte)255,
                R = r_enabled ? r : (byte)0,
                G = g_enabled ? g : (byte)0,
                B = b_enabled ? b : (byte)0
            };

            colors.Add(new_color);
            add_command.RaiseCanExecuteChanged();
        }

        private bool op_add_color()
        {
            var new_color = new color_model
            {
                A = a_enabled ? a : (byte)255,
                R = r_enabled ? r : (byte)0,
                G = g_enabled ? g : (byte)0,
                B = b_enabled ? b : (byte)0
            };

            return !colors.Any(c => c.Equals(new_color));
        }

        private void delete_color()
        {
            if (selected_color_model != null)
            {
                colors.Remove(selected_color_model);
                delete_command.RaiseCanExecuteChanged();
            }
        }

        private bool op_delete_color()
        {
            return selected_color_model != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
