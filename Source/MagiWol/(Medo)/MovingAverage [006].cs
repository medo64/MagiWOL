//Copyright (c) 2007 Josip Medved <jmedved@jmedved.com>

//2007-09-19: Moved to common.
//2008-01-03: Added Resources.
//2008-01-05: Moved to Medo.Math.Averaging.
//2008-04-11: Cleaned code to match FxCop 1.36 beta 2 (DoNotRaiseExceptionsInUnexpectedLocations).
//2010-05-14: Changed namespace from Medo.Math.Averaging to Medo.Math.
//2011-03-05: Moved to Medo.Math.


namespace Medo.Math {

	/// <summary>
	/// Calculates moving average for added items.
	/// </summary>
	public class MovingAverage {

		private System.Collections.Generic.List<double> _items = new System.Collections.Generic.List<double>();
		private int _maxCount;


		/// <summary>
		/// Creates new instance with total of 10 items.
		/// </summary>
		public MovingAverage()
			: this(10) {
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="maxCount">Number of items to use for calculation.</param>
		public MovingAverage(int maxCount) {
			this._maxCount = maxCount;
		}


		/// <summary>
		/// Adds an item.
		/// </summary>
		/// <param name="value">Value to be added.</param>
		public void Add(double value) {
			this._items.Add(value);
			while (this._items.Count > this._maxCount) {
				this._items.RemoveAt(0);
			}
		}

		/// <summary>
		/// Resets average.
		/// </summary>
		public void Clear() {
			this._items.Clear();
		}

		/// <summary>
		/// Gets whether there are items inside.
		/// </summary>
		public bool IsEmpty {
			get { return this._items.Count == 0; }
		}


		/// <summary>
		/// Returns average or NaN if there is no data to calculate.
		/// </summary>
		public double Average {
			get {
				if (this.IsEmpty) {
					return double.NaN;
				} else {
					int count = this._items.Count;
					double sum = 0;
					for (int i = 0; i < count; i++) {
						sum += this._items[i];
					}
					return (sum / count);
				}
			}
		}

	}

}