using System;
using System.Collections.Generic;
using System.Linq;
using Discord;

namespace DiscordNetPlus.Interaction
{
    public class RelationMemory
    {
        private Dictionary<object, Dictionary<object, IConversation>> Memories { get; } = new Dictionary<object, Dictionary<object, IConversation>>();


        private Dictionary<object, IConversation> RecallPerson(object scopeKey)
            => Memories.FirstOrDefault(m => m.Key == scopeKey).Value;
        private KeyValuePair<object, IConversation> RecallConversation(Dictionary<object, IConversation> person, object topicKey)
            => person.FirstOrDefault(t => t.Key == topicKey);

        public IConversation Recall(object scopeKey, object topicKey)
        {
            var scope = RecallPerson(scopeKey);
            var conv = RecallConversation(scope, topicKey);

            //if (conv.Equals(default(KeyValuePair<object, IConversation>)))
            //    return null;

            return conv.Value;
        }

        public void Memorise(object scopeKey, IConversation conv)
        {
            if (RecallPerson(scopeKey) == null)
            {
                var personMem = new Dictionary<object, IConversation>();
                personMem.Add(conv.Topic, conv);

                Memories.Add(scopeKey, personMem);

                return;
            }

            Memories[scopeKey][conv.Topic] = conv;
        }

        public void EraseMemoryAt(object scopeKey, object conversationKey)
        {
            
        }
    }
    

    public interface IConversation
    {
        object Topic { get; set; }
    }

    public class UserWideConversation : IConversation
    {
        public object Topic { get; set; }
    }

    public class ChannelWideConversation : IConversation
    {
        public object Topic { get; set; }
    }

}