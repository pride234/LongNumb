using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LongArithmetic {


//----------------------ProgramClass--------------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------ProgramClass--------------------------------------------------------------------------------------------------------------------------------------------------|


	class Program {

//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
			
		static void Main(string[] args) {

		BigInteger a = new BigInteger ("ADFFAFDFAFDFADAD321651513151315ADAFDFADFFAD24654564531ADADFADAD31351AD");
		BigInteger b = new BigInteger ("ADFFADFFADFFAFDFFFFADFFFAD664564FFADFFFAFDF"); 
		BigInteger c = new BigInteger ("567B1E8281B6CF8D02C4B193A461B04737CB05B0A0C2CDEB1D6EDEC7928731C1AAC6716AE1DE1B8E503DE696E90CF99A8E7E6186D89F6F90360728C725808FD0");
		//BigInteger d = new BigInteger(a.BinaryNotion(a.ToBinary()).Deduct(b.BinaryNotion(b.ToBinary())).ToHex());
		//Console.WriteLine(c.BinaryArray(a.ToBinary()).ToHex());
		//Console.WriteLine(a.BinaryNotion(a.ToBinary()).Deduct(b.BinaryNotion(b.ToBinary())).ToHex());
		
		//Console.WriteLine(c.HexNotion("567B1E8281B6CF8D02C4B193A461B04737CB05B0A0C2CDEB1D6EDEC7928731C1AAC6716AE1DE1B8E503DE696E90CF99A8E7E6186D89F6F90360728C725808FD0").ToHexHex());
		
		Console.WriteLine(a.Gorner(b).ToHexHex());
		Console.ReadKey();
		}
	}


//----------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------BigIntegerClass-----------------------------------------------------------------------------------------------------------------------------------------------|


	class BigInteger {

		private ulong[] number;
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static ulong[] Padding (ulong[] mas, int len) {
		
			ulong[] pad = new ulong[len];

			for (int i = 0; i<mas.Length; i++) pad[i] = mas[i];
			return pad;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static BigInteger Humanresult (ulong[] array, int maxlen){
			
			BigInteger result = new BigInteger();
			int i = maxlen-1;
			
			while (array[i] == 0) {
				i--;
				if (i == -1) return result;
			}

			ulong[] humanresult = new ulong[i+1];
			for (int j = 0; j<humanresult.Length; j++) humanresult[j] = array[j];
			result.number = humanresult;
			return result;			 
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger AuxiliaryMultForGorner (BigInteger num, ulong b){
						
			ulong carry = 0, temp;
			ulong[] array = new ulong[num.number.Length+1];

			for (int i = 0; i < num.number.Length; i++) {
				temp = num.number[i]*b + carry;
				array[i] = temp & 0xF;
				carry = temp >> 0x10;
			}

			array[num.number.Length] = carry;
			return Humanresult(array, num.number.Length+1);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private BigInteger AuxiliaryMult (BigInteger num, ulong b){
						
			ulong carry = 0, temp;
			ulong[] array = new ulong[num.number.Length+1];

			for (int i = 0; i < num.number.Length; i++) {
				temp = num.number[i]*b + carry;
				array[i] = temp & 0xFFFFFFFF;
				carry = temp >> 0x20;
			}

			array[num.number.Length] = carry;
			return Humanresult(array, num.number.Length+1);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
		
		private ulong[] ToShiftDigitsToRight (ulong[] array, int i){
			
			//ulong arrlen = Convert.ToUInt64(array.Length);
			ulong[] a = new ulong[array.Length + i];

			for (int j = 0; j < array.Length; j++){
				a[j+i] = array[j];
			}
			
			return a;
		}		
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger (){
		
			number = new ulong[1];
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger (string hex){
			
			int dig = hex.Length/8;
			if (hex.Length%8 != 0) dig += 1;
			number = new ulong[dig];
			int i = 0;

			for (; i < dig-1; i++) number[i] = Convert.ToUInt64(hex.Substring(hex.Length - (8*(i+1)), 8), 16);
			if (8*i < hex.Length)  number[i] = Convert.ToUInt64(hex.Substring(0, hex.Length - (8*i)), 16);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger HexNotion(string hex) {
			
			BigInteger A = new BigInteger();

			A.number = new ulong[hex.Length];

			for(int i = hex.Length-1; i >= 0;i--) A.number[hex.Length-1-i] = Convert.ToUInt64(hex.Substring(i, 1), 16);
			return A;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger BinaryNotion (string binary) {

			int dig = binary.Length/32;
			if (binary.Length%32 != 0) dig += 1;
			this.number = new ulong[dig];
			int i = 0;

			for (; i < dig-1; i++) this.number[i] = Convert.ToUInt64(binary.Substring(binary.Length - (32*(i+1)), 32), 2);
			if (32*i < binary.Length)  this.number[i] = Convert.ToUInt64(binary.Substring(0, binary.Length - (32*i)), 2);
			
			return this;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public string ToBinary() {
			
			string result = "";
			result += Convert.ToString((long)number[number.Length-1], 2);
			for (int i = number.Length-2; i >= 0; i--) result += Convert.ToString((long)number[i], 2).PadLeft(32,'0');
			return result;
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public string ToHexHex() {
			
			string result = "";
			result += number[number.Length-1].ToString("X");
			for (int i = number.Length-2; i >= 0; i--) result += number[i].ToString("X");//.PadLeft(8, '0');
			return result;
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public string ToHex() {
			
			string result = "";
			result += number[number.Length-1].ToString("X");
			for (int i = number.Length-2; i >= 0; i--) result += number[i].ToString("X8");//.PadLeft(8, '0');
			return result;
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public int Comprassion(BigInteger num) {
			int len = this.number.Length >= num.number.Length ? this.number.Length : num.number.Length;
			ulong[] a = Padding(this.number, len);
			ulong[] b = Padding(num.number, len);

			int i = a.Length - 1;
			if (a[i] > b[i]) return 1;
			
			while (a[i] == b[i]) {
				i -= 1;
				if (i == -1) return 0;
				else if (a[i] > b[i]) return 1;				
			}			
			
			return -1;
		} 
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public BigInteger Add(BigInteger num) {
			
			int maxlen = 1 + (this.number.Length >= num.number.Length ? this.number.Length : num.number.Length);
			ulong[] a = Padding(this.number, maxlen);
			ulong[] b = Padding(num.number, maxlen);
			ulong[] array = new ulong[maxlen];
			ulong carry = 0, temp;

			for (int i = 0; i<maxlen; i++){
				temp = a[i] + b[i] + carry;
				array[i] = temp & 0xFFFFFFFF;
				carry = temp >> 0x20;
			}
		
			return Humanresult(array, maxlen);	
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Deduct(BigInteger num) {
			
			if (this.Comprassion(num) == -1) {
				Console.WriteLine("Answer is a negative number! The secong operand is bigger that the first. Please, correct this. Look at them: \n");
				Console.WriteLine(this.ToHex());
				return num;
			}

			int maxlen = this.number.Length >= num.number.Length ? this.number.Length : num.number.Length;
			ulong[] a = Padding(this.number, maxlen);
			ulong[] b = Padding(num.number, maxlen);
			ulong[] array = new ulong[maxlen];
			ulong borrow = 0;
			
			for (int i = 0; i<maxlen; i++){
				if ((a[i] - borrow) < b[i]) {
					array[i] = a[i] - b[i] - borrow + 0xFFFFFFFF + 1;
					borrow = 1;
				}
				else {
					array[i] = a[i] - b[i] - borrow;
					borrow = 0;
				}
			}

			return Humanresult(array, maxlen);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger MultiplyForGorner (BigInteger num) {
						
			BigInteger temp = new BigInteger();
			BigInteger result = new BigInteger();
		
			for (int i = 0; i<num.number.Length; i++){
				temp = AuxiliaryMultForGorner(this, num.number[i]);
				temp.number = ToShiftDigitsToRight(temp.number, i);
				result = result.Add(temp);
			}

			return result;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Multiply (BigInteger num) {
						
			BigInteger temp = new BigInteger();
			BigInteger result = new BigInteger();
		
			for (int i = 0; i<num.number.Length; i++){
				temp = AuxiliaryMult(this, num.number[i]);
				temp.number = ToShiftDigitsToRight(temp.number, i);
				result = result.Add(temp);
			}

			return result;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public void DivideBy(BigInteger num) {

			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, num.number.Length);
			if (num.Comprassion(zero) == 0) {
				Console.WriteLine("Exeption! Division by zero!");
				return;
			}
			
			BigInteger R = new BigInteger();
			BigInteger Q = new BigInteger();
			BigInteger C = new BigInteger();
			
			
			Q.number = Padding(Q.number, this.number.Length);
			R = this;
			
			int k = num.ToBinary().Length;
			int t;
			
			string replacebit;
			string shift;

			while(R.Comprassion(num) != -1){
				shift = num.ToBinary();
				t = R.ToBinary().Length;
				for (int i = 0; i< t-k; i++) shift += "0";
				C = C.BinaryNotion(shift);
				if (R.Comprassion(C) == -1){
					t -= 1;
					shift = num.ToBinary();
					for (int i = 0; i< t-k; i++) shift += "0";
					C = C.BinaryNotion(shift);
				}
				
				R = R.Deduct(C);
				replacebit = "1";
				for (int i = 0; i<(t-k)%32; i++) replacebit += "0";
				Q.number[(t-k)/32] = Q.number[(t-k)/32] | Convert.ToUInt64(replacebit, 2);
			}
		
			Console.WriteLine("Integer part from division is: " + Humanresult(Q.number, Q.number.Length).ToHex());
			Console.WriteLine("Modulo part from division is: " +  Humanresult(R.number, R.number.Length).ToHex());
		}
		//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public BigInteger Gorner(BigInteger num) {

			
			BigInteger C = new BigInteger("1");
			BigInteger zero = new BigInteger();
			zero.number = Padding(zero.number, num.number.Length);
			if (num.Comprassion(zero) == 0) {
				return C;
			}
			BigInteger[] D = new BigInteger[16];
		 
			D[0] = C.HexNotion(C.ToHex());; 
			D[1] = this;
			Console.WriteLine("A Ondrey na obed podzalupniy tworog est!!!");//by twoya Mamasha
			for (int i = 2; i < 16; i++) D[i] = D[i-1].Multiply(this);
				
			for (int i = 1; i < 16; i++) D[i] = D[i].HexNotion(D[i].ToHex());
	
			
			for (int i = num.HexNotion(num.ToHex()).number.Length - 1; i>=0; i --) {
				C = C.MultiplyForGorner(D[num.HexNotion(num.ToHex()).number[i]]);	
				if (i != 0) {
					for (int k = 1; k>4; k++) C = C.MultiplyForGorner(C);
				}
			}
			return Humanresult(C.number, C.number.Length);
		}
//----------------------EndOfBigIntegerClass------------------------------------------------------------------------------------------------------------------------------------------|
//----------------------EndOfBigIntegerClass------------------------------------------------------------------------------------------------------------------------------------------|
	}
	//----------------------EndOfnamespase------------------------------------------------------------------------------------------------------------------------------------------------|
	//----------------------EndOfnamespase------------------------------------------------------------------------------------------------------------------------------------------------|
}
