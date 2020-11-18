using System;

namespace StarkBank.Utils
{
    public class StarkBankDate
    {
        public DateTime? Value;

        public StarkBankDate(DateTime? value) {
            Value = value;
        }

        public override string ToString() {
            if (Value == null)
                return null;
            DateTime value = (DateTime) Value;
            return value.ToString("yyyy-MM-dd");
        }
    }

    public class StarkBankDateTime
    {
        public DateTime? Value;

        public StarkBankDateTime(DateTime? value)
        {
            Value = value;
        }

        public override string ToString()
        {
            if (Value == null)
                return null;
            DateTime value = (DateTime)Value;
            return value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffff") + "+00:00";
        }
    }
}
