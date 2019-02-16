/*THIS LIBRARY EXTRACTS PEPTIDES FROM THE GIVEN SEQUENCE
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Qazi.Peptides.PeptideGenerator
{
    public class PeptideData : IDisposable 
    {
        public string ExtendedPeptideSequence;
        public List<string> PeptideSequence;

        public PeptideData()
        {
            PeptideSequence = new List<string>();
            ExtendedPeptideSequence = "";
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (PeptideSequence != null)
            {
                PeptideSequence.Clear();
                PeptideSequence = null;
            }
        }

        #endregion
    }

    [Serializable]
    public class PeptideCutter
    {
        public static PeptideData ToPeptideFromExtendedSequence(string sequence, int position, int sizeOfOneSide)
        {
            char[] sep = { ',' };
            string[] sequenceArray;
            sequenceArray = sequence.Split(sep);
            return GeneratePeptide(sequenceArray, position, sizeOfOneSide);
        }

        private static PeptideData GeneratePeptide(string[] sequenceArray, int position, int _sizeOfOneSide)
        {
            PeptideData peptidedata = new PeptideData();
            int start, end;
            const string dash = "-";
            string amino;
            string _peptideSequence;
            
            position--; //Convert position number to index. Here after variable position will refer to the index in sequence
            start = position - _sizeOfOneSide;
            end = position + _sizeOfOneSide;
            _peptideSequence = "";
            int indexInPeptide = -1 * _sizeOfOneSide;

            for (int indexInSequence = start; indexInSequence <= end; indexInSequence++)
            {
                // transverse in sequence to get peptide
                if (indexInSequence < 0)
                    amino = dash;
                else if (indexInSequence >= sequenceArray.Length)
                    amino = dash;
                else
                    amino = sequenceArray[indexInSequence];

                _peptideSequence = _peptideSequence + amino;
                peptidedata.PeptideSequence.Add(amino);

                if (indexInSequence < end == true)
                    _peptideSequence = _peptideSequence + ",";
                
            }

            peptidedata.ExtendedPeptideSequence = _peptideSequence;
            return peptidedata;
        }


        public static PeptideData ToPeptideFromStandardSequence(string sequence, int position, int sizeOfOneSide)
        {
            string[] sequenceArray;
            sequenceArray = new string[sequence.Length];

            for (int index = 0; index < sequence.Length; index++)
            {
                sequenceArray[index] = sequence[index].ToString();
            }

            return GeneratePeptide(sequenceArray, position, sizeOfOneSide);
        }

    }
}
