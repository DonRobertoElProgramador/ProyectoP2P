using SendIt_;
using System;
using System.Collections.ObjectModel;

public interface iController
{
	void addMessage(Message message);
	ObservableCollection<Message> getMessages();
}
