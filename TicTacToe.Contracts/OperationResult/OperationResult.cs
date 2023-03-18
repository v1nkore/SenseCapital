using TicTacToe.Domain.Extensions;

namespace TicTacToe.Contracts.OperationResult
{
	public interface IOperationResult<TValue>
	{
		public TValue? Value { get; init; }
		public ICollection<string>? Metadata { get; set; }
		public bool Ok { get; set; }
	}

	public class OperationResult<TValue> : IOperationResult<TValue>
	{
		public TValue? Value { get; init; }
		public bool Ok { get; set; }
		public ICollection<string> Metadata { get; set; }

		public OperationResult(TValue value, bool ok)
		{
			Value = value;
			Ok = ok;
		}

		public OperationResult<TValue> AddMetadata(string metadata)
		{
			Metadata = Metadata.InitStructIfNullAndAdd(metadata);

			return this;
		}

		public OperationResult<TValue> AddMetadataRange(IEnumerable<string> metadata)
		{
			Metadata = Metadata.InitStructIfNullAndAddRange(metadata);

			return this;
		}
	}
}
