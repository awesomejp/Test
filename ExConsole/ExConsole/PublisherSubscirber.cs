using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ExConsole
{
	[TestFixture]
	public class PublisherSubscirber
	{
		public NestedPublisher NestedPublisherProp
		{
			get
			{
				return new NestedPublisher();
			}
		}

		public class NestedPublisher
		{
			public event EventHandler Event;
			private NestedSubscriber _subscriber;

			[TestCase]
			public void CreateSubscriber()
			{
				_subscriber = new NestedSubscriber(this);
			}

			public void DestroySubscriber()
			{
				_subscriber.Dispose();
				_subscriber = null;
			}
		}

		public class NestedSubscriber
		{
			private NestedPublisher _publisher;

			public NestedSubscriber(NestedPublisher publisher)
			{
				_publisher = publisher;
				publisher.Event += this.Handler;
			}

			~NestedSubscriber()
			{
				Console.WriteLine("Subscriber disposed");
			}

			private void Handler(object sender, EventArgs e)
			{

			}

			public void Dispose()
			{
				_publisher.Event -= this.Handler;
			}
		}
	}
}
