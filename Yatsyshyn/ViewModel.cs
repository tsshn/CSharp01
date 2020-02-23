using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Yatsyshyn
{
    internal sealed class ViewModel : INotifyPropertyChanged
    {
        private DateTime _date = DateTime.Now;
        private int _age = -1;
        private string _westernSign;
        private string _chineseSign;

        private readonly string[] _westernSigns =
        {
            "Aquarius ♒", "Pisces ♓", "Aries ♈", "Taurus ♉", "Gemini ♊", "Cancer ♋", "Leo ♌", "Virgo ♍", "Libra ♍",
            "Scorpio ♏",
            "Sagittarius ♐", "Capricorn ♑"
        };

        private readonly string[] _chineseSigns =
        {
            "Rat 🐀", "Ox 🐂", "Tiger 🐅", "Rabbit 🐇", "Dragon 🐉", "Snake 🐍", "Horse 🐎", "Goat 🐐", "Monkey 🐒",
            "Rooster 🐓", "Dog 🐕", "Pig 🐖"
        };

        public string Age
        {
            get
            {
                if (_age == -1)
                    return $"Age:";
                return $"Age: {_age}";
            }
        }

        public string Chinese
        {
            get { return $"Chinese sign: {_chineseSign}"; }

            set
            {
                _chineseSign = value;
                OnPropertyChanged();
            }
        }

        public string Western
        {
            get { return $"Western sign: {_westernSign}"; }

            set
            {
                _westernSign = value;
                OnPropertyChanged();
            }
        }

        private string CalcChineseHoroscope()
        {
            var index = Math.Abs(_date.Year - 1900) % 12;
            return _chineseSigns[index];
        }

        private string CalcWesternHoroscope()
        {
            var month = _date.Month;
            var day = _date.Day;
            switch (month)
            {
                case 01 when day >= 20:
                case 02 when day <= 18:
                    return _westernSigns[0];
                case 02 when day >= 19:
                case 03 when day <= 20:
                    return _westernSigns[1];
                case 03 when day >= 21:
                case 04 when day <= 19:
                    return _westernSigns[2];
                case 04 when day >= 20:
                case 05 when day <= 20:
                    return _westernSigns[3];
                case 05 when day >= 21:
                case 06 when day <= 20:
                    return _westernSigns[4];
                case 06 when day >= 21:
                case 07 when day <= 22:
                    return _westernSigns[5];
                case 07 when day >= 23:
                case 08 when day <= 22:
                    return _westernSigns[6];
                case 08 when day >= 23:
                case 09 when day <= 22:
                    return _westernSigns[7];
                case 09 when day >= 23:
                case 10 when day <= 22:
                    return _westernSigns[8];
                case 10 when day >= 23:
                case 11 when day <= 21:
                    return _westernSigns[9];
                case 11 when day >= 22:
                case 12 when day <= 21:
                    return _westernSigns[10];
                case 12 when day >= 22:
                case 01 when day <= 19:
                    return _westernSigns[11];
                default: return "";
            }
        }

        private async void Processor(object obj)
        {
            await Task.Run(() => _age = CalcAge(_date));
            if (!CheckAge(_date)) return;
            await Task.Run(() => Western = CalcWesternHoroscope());
            await Task.Run(() => Chinese = CalcChineseHoroscope());
            OnPropertyChanged(nameof(Age));
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private int CalcAge(DateTime birthdayDate)
        {
            if (_date.Year == DateTime.Today.Year && _date.Month == DateTime.Today.Month &&
                _date.Day == DateTime.Today.Day) return 0;
            var age = DateTime.Today.Year - birthdayDate.Year;
            if (birthdayDate > DateTime.Today.AddYears(-age)) age--;
            return age;
        }

        private bool CheckAge(DateTime date)
        {
            if (_age < 0)
            {
                MessageBox.Show("Birth date can not be set in the future.");
                return false;
            }

            if (_age > 135)
            {
                MessageBox.Show("Your age can not be above 135.");
                return false;
            }

            if (date.Day == DateTime.Today.Day && date.Month == DateTime.Today.Month)
            {
                MessageBox.Show("Happy Birthday");
                return true;
            }

            return true;
        }

        private RelayCommand<object> _command;

        public RelayCommand<object> Command => _command ?? (_command = new RelayCommand<object>(Processor));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}