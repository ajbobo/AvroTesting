namespace AvroFunctions
{
    /* This is where I put functions that my generated classes need, but that isn't in the generated code */

    partial class RecordId
    {
        public override string ToString()
        {
            return string.Format("[id_num: {0}  id_str: {1}]", this.id_num, this.id_str);
        }

        public override bool Equals(object obj)
        {
            if (obj is RecordId other)
            {
                return
                    (other.id_num == this.id_num) &&
                    other.id_str.Equals(this.id_str);
            }

            return base.Equals(obj);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    partial class DataRecord
    {
        public override string ToString()
        {
            return string.Format("[{0} {1} ({2}) {3}]", this.one, this.two, this.id, this.three);
        }

        public override bool Equals(object obj)
        {
            if (obj is DataRecord other)
            {
                return
                    (other.one == this.one) &&
                    (other.two == this.two) &&
                    other.three.Equals(this.three) &&
                    other.id.Equals(this.id);
 
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
