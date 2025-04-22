using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
	public partial class MerchantSession
	{
		public partial class AllowedInstallment : StarkCore.Utils.SubResource
        {
			public int TotalAmount { get; }
			public int Count { get; }

			public AllowedInstallment(int totalAmount, int count)
			{
				TotalAmount = totalAmount;
				Count = count;
			}

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "AllowedInstallment", resourceMaker: ResourceMaker);
            }

            internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
            {
                int totalAmount = json.totalAmount;
                int count = json.count;

                return new AllowedInstallment(
                    totalAmount: totalAmount,
                    count: count
                );
            }
        }
	}
}

