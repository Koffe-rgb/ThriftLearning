/**
 * <auto-generated>
 * Autogenerated by Thrift Compiler (0.18.1)
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 * </auto-generated>
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE0017  // object init can be simplified
#pragma warning disable IDE0028  // collection init can be simplified
#pragma warning disable IDE1006  // parts of the code use IDL spelling
#pragma warning disable CA1822   // empty DeepCopy() methods still non-static
#pragma warning disable IDE0083  // pattern matching "that is not SomeType" requires net5.0 but we still support earlier versions

namespace ThriftSpecification.Shared
{
  public partial class CalculatorService
  {
    public interface IAsync
    {
      global::System.Threading.Tasks.Task<int> Evaluate(global::ThriftSpecification.Shared.Work work, CancellationToken cancellationToken = default);

      global::System.Threading.Tasks.Task SayHello(string name, CancellationToken cancellationToken = default);

    }


    public class Client : TBaseClient, IDisposable, IAsync
    {
      public Client(TProtocol protocol) : this(protocol, protocol)
      {
      }

      public Client(TProtocol inputProtocol, TProtocol outputProtocol) : base(inputProtocol, outputProtocol)
      {
      }

      public async global::System.Threading.Tasks.Task<int> Evaluate(global::ThriftSpecification.Shared.Work work, CancellationToken cancellationToken = default)
      {
        await send_Evaluate(work, cancellationToken);
        return await recv_Evaluate(cancellationToken);
      }

      public async global::System.Threading.Tasks.Task send_Evaluate(global::ThriftSpecification.Shared.Work work, CancellationToken cancellationToken = default)
      {
        await OutputProtocol.WriteMessageBeginAsync(new TMessage("Evaluate", TMessageType.Call, SeqId), cancellationToken);
        
        var tmp10 = new InternalStructs.Evaluate_args() {
          Work = work,
        };
        
        await tmp10.WriteAsync(OutputProtocol, cancellationToken);
        await OutputProtocol.WriteMessageEndAsync(cancellationToken);
        await OutputProtocol.Transport.FlushAsync(cancellationToken);
      }

      public async global::System.Threading.Tasks.Task<int> recv_Evaluate(CancellationToken cancellationToken = default)
      {
        
        var tmp11 = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
        if (tmp11.Type == TMessageType.Exception)
        {
          var tmp12 = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
          await InputProtocol.ReadMessageEndAsync(cancellationToken);
          throw tmp12;
        }

        var tmp13 = new InternalStructs.Evaluate_result();
        await tmp13.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        if (tmp13.__isset.success)
        {
          return tmp13.Success;
        }
        if (tmp13.__isset.e)
        {
          throw tmp13.E;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "Evaluate failed: unknown result");
      }

      public async global::System.Threading.Tasks.Task SayHello(string name, CancellationToken cancellationToken = default)
      {
        await send_SayHello(name, cancellationToken);
      }

      public async global::System.Threading.Tasks.Task send_SayHello(string name, CancellationToken cancellationToken = default)
      {
        await OutputProtocol.WriteMessageBeginAsync(new TMessage("SayHello", TMessageType.Oneway, SeqId), cancellationToken);
        
        var tmp14 = new InternalStructs.SayHello_args() {
          Name = name,
        };
        
        await tmp14.WriteAsync(OutputProtocol, cancellationToken);
        await OutputProtocol.WriteMessageEndAsync(cancellationToken);
        await OutputProtocol.Transport.FlushAsync(cancellationToken);
      }

    }

    public class AsyncProcessor : ITAsyncProcessor
    {
      private readonly IAsync _iAsync;
      private readonly ILogger<AsyncProcessor> _logger;

      public AsyncProcessor(IAsync iAsync, ILogger<AsyncProcessor> logger = default)
      {
        _iAsync = iAsync ?? throw new ArgumentNullException(nameof(iAsync));
        _logger = logger;
        processMap_["Evaluate"] = Evaluate_ProcessAsync;
        processMap_["SayHello"] = SayHello_ProcessAsync;
      }

      protected delegate global::System.Threading.Tasks.Task ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken);
      protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

      public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot)
      {
        return await ProcessAsync(iprot, oprot, CancellationToken.None);
      }

      public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
      {
        try
        {
          var msg = await iprot.ReadMessageBeginAsync(cancellationToken);

          processMap_.TryGetValue(msg.Name, out var fn);

          if (fn == null)
          {
            await TProtocolUtil.SkipAsync(iprot, TType.Struct, cancellationToken);
            await iprot.ReadMessageEndAsync(cancellationToken);
            var x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
            await oprot.WriteMessageBeginAsync(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID), cancellationToken);
            await x.WriteAsync(oprot, cancellationToken);
            await oprot.WriteMessageEndAsync(cancellationToken);
            await oprot.Transport.FlushAsync(cancellationToken);
            return true;
          }

          await fn(msg.SeqID, iprot, oprot, cancellationToken);

        }
        catch (IOException)
        {
          return false;
        }

        return true;
      }

      public async global::System.Threading.Tasks.Task Evaluate_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
      {
        var tmp15 = new InternalStructs.Evaluate_args();
        await tmp15.ReadAsync(iprot, cancellationToken);
        await iprot.ReadMessageEndAsync(cancellationToken);
        var tmp16 = new InternalStructs.Evaluate_result();
        try
        {
          try
          {
            tmp16.Success = await _iAsync.Evaluate(tmp15.Work, cancellationToken);
          }
          catch (global::ThriftSpecification.Shared.InvalidOperationException tmp17)
          {
            tmp16.E = tmp17;
          }
          await oprot.WriteMessageBeginAsync(new TMessage("Evaluate", TMessageType.Reply, seqid), cancellationToken); 
          await tmp16.WriteAsync(oprot, cancellationToken);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception tmp18)
        {
          var tmp19 = $"Error occurred in {GetType().FullName}: {tmp18.Message}";
          if(_logger != null)
            _logger.LogError("{Exception}, {Message}", tmp18, tmp19);
          else
            Console.Error.WriteLine(tmp19);
          var tmp20 = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
          await oprot.WriteMessageBeginAsync(new TMessage("Evaluate", TMessageType.Exception, seqid), cancellationToken);
          await tmp20.WriteAsync(oprot, cancellationToken);
        }
        await oprot.WriteMessageEndAsync(cancellationToken);
        await oprot.Transport.FlushAsync(cancellationToken);
      }

      public async global::System.Threading.Tasks.Task SayHello_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
      {
        var tmp21 = new InternalStructs.SayHello_args();
        await tmp21.ReadAsync(iprot, cancellationToken);
        await iprot.ReadMessageEndAsync(cancellationToken);
        try
        {
          await _iAsync.SayHello(tmp21.Name, cancellationToken);
        }
        catch (TTransportException)
        {
          throw;
        }
        catch (Exception tmp23)
        {
          var tmp24 = $"Error occurred in {GetType().FullName}: {tmp23.Message}";
          if(_logger != null)
            _logger.LogError("{Exception}, {Message}", tmp23, tmp24);
          else
            Console.Error.WriteLine(tmp24);
        }
      }

    }

    public class InternalStructs
    {

      public partial class Evaluate_args : TBase
      {
        private global::ThriftSpecification.Shared.Work _work;

        public global::ThriftSpecification.Shared.Work Work
        {
          get
          {
            return _work;
          }
          set
          {
            __isset.work = true;
            this._work = value;
          }
        }


        public Isset __isset;
        public struct Isset
        {
          public bool work;
        }

        public Evaluate_args()
        {
        }

        public Evaluate_args DeepCopy()
        {
          var tmp25 = new Evaluate_args();
          if((Work != null) && __isset.work)
          {
            tmp25.Work = (global::ThriftSpecification.Shared.Work)this.Work.DeepCopy();
          }
          tmp25.__isset.work = this.__isset.work;
          return tmp25;
        }

        public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
          iprot.IncrementRecursionDepth();
          try
          {
            TField field;
            await iprot.ReadStructBeginAsync(cancellationToken);
            while (true)
            {
              field = await iprot.ReadFieldBeginAsync(cancellationToken);
              if (field.Type == TType.Stop)
              {
                break;
              }

              switch (field.ID)
              {
                case 1:
                  if (field.Type == TType.Struct)
                  {
                    Work = new global::ThriftSpecification.Shared.Work();
                    await Work.ReadAsync(iprot, cancellationToken);
                  }
                  else
                  {
                    await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  }
                  break;
                default: 
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  break;
              }

              await iprot.ReadFieldEndAsync(cancellationToken);
            }

            await iprot.ReadStructEndAsync(cancellationToken);
          }
          finally
          {
            iprot.DecrementRecursionDepth();
          }
        }

        public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
          oprot.IncrementRecursionDepth();
          try
          {
            var tmp26 = new TStruct("Evaluate_args");
            await oprot.WriteStructBeginAsync(tmp26, cancellationToken);
            var tmp27 = new TField();
            if((Work != null) && __isset.work)
            {
              tmp27.Name = "work";
              tmp27.Type = TType.Struct;
              tmp27.ID = 1;
              await oprot.WriteFieldBeginAsync(tmp27, cancellationToken);
              await Work.WriteAsync(oprot, cancellationToken);
              await oprot.WriteFieldEndAsync(cancellationToken);
            }
            await oprot.WriteFieldStopAsync(cancellationToken);
            await oprot.WriteStructEndAsync(cancellationToken);
          }
          finally
          {
            oprot.DecrementRecursionDepth();
          }
        }

        public override bool Equals(object that)
        {
          if (!(that is Evaluate_args other)) return false;
          if (ReferenceEquals(this, other)) return true;
          return ((__isset.work == other.__isset.work) && ((!__isset.work) || (global::System.Object.Equals(Work, other.Work))));
        }

        public override int GetHashCode() {
          int hashcode = 157;
          unchecked {
            if((Work != null) && __isset.work)
            {
              hashcode = (hashcode * 397) + Work.GetHashCode();
            }
          }
          return hashcode;
        }

        public override string ToString()
        {
          var tmp28 = new StringBuilder("Evaluate_args(");
          int tmp29 = 0;
          if((Work != null) && __isset.work)
          {
            if(0 < tmp29++) { tmp28.Append(", "); }
            tmp28.Append("Work: ");
            Work.ToString(tmp28);
          }
          tmp28.Append(')');
          return tmp28.ToString();
        }
      }


      public partial class Evaluate_result : TBase
      {
        private int _success;
        private global::ThriftSpecification.Shared.InvalidOperationException _e;

        public int Success
        {
          get
          {
            return _success;
          }
          set
          {
            __isset.success = true;
            this._success = value;
          }
        }

        public global::ThriftSpecification.Shared.InvalidOperationException E
        {
          get
          {
            return _e;
          }
          set
          {
            __isset.e = true;
            this._e = value;
          }
        }


        public Isset __isset;
        public struct Isset
        {
          public bool success;
          public bool e;
        }

        public Evaluate_result()
        {
        }

        public Evaluate_result DeepCopy()
        {
          var tmp30 = new Evaluate_result();
          if(__isset.success)
          {
            tmp30.Success = this.Success;
          }
          tmp30.__isset.success = this.__isset.success;
          if((E != null) && __isset.e)
          {
            tmp30.E = (global::ThriftSpecification.Shared.InvalidOperationException)this.E.DeepCopy();
          }
          tmp30.__isset.e = this.__isset.e;
          return tmp30;
        }

        public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
          iprot.IncrementRecursionDepth();
          try
          {
            TField field;
            await iprot.ReadStructBeginAsync(cancellationToken);
            while (true)
            {
              field = await iprot.ReadFieldBeginAsync(cancellationToken);
              if (field.Type == TType.Stop)
              {
                break;
              }

              switch (field.ID)
              {
                case 0:
                  if (field.Type == TType.I32)
                  {
                    Success = await iprot.ReadI32Async(cancellationToken);
                  }
                  else
                  {
                    await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  }
                  break;
                case 1:
                  if (field.Type == TType.Struct)
                  {
                    E = new global::ThriftSpecification.Shared.InvalidOperationException();
                    await E.ReadAsync(iprot, cancellationToken);
                  }
                  else
                  {
                    await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  }
                  break;
                default: 
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  break;
              }

              await iprot.ReadFieldEndAsync(cancellationToken);
            }

            await iprot.ReadStructEndAsync(cancellationToken);
          }
          finally
          {
            iprot.DecrementRecursionDepth();
          }
        }

        public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
          oprot.IncrementRecursionDepth();
          try
          {
            var tmp31 = new TStruct("Evaluate_result");
            await oprot.WriteStructBeginAsync(tmp31, cancellationToken);
            var tmp32 = new TField();

            if(this.__isset.success)
            {
              tmp32.Name = "Success";
              tmp32.Type = TType.I32;
              tmp32.ID = 0;
              await oprot.WriteFieldBeginAsync(tmp32, cancellationToken);
              await oprot.WriteI32Async(Success, cancellationToken);
              await oprot.WriteFieldEndAsync(cancellationToken);
            }
            else if(this.__isset.e)
            {
              if (E != null)
              {
                tmp32.Name = "E";
                tmp32.Type = TType.Struct;
                tmp32.ID = 1;
                await oprot.WriteFieldBeginAsync(tmp32, cancellationToken);
                await E.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
              }
            }
            await oprot.WriteFieldStopAsync(cancellationToken);
            await oprot.WriteStructEndAsync(cancellationToken);
          }
          finally
          {
            oprot.DecrementRecursionDepth();
          }
        }

        public override bool Equals(object that)
        {
          if (!(that is Evaluate_result other)) return false;
          if (ReferenceEquals(this, other)) return true;
          return ((__isset.success == other.__isset.success) && ((!__isset.success) || (global::System.Object.Equals(Success, other.Success))))
            && ((__isset.e == other.__isset.e) && ((!__isset.e) || (global::System.Object.Equals(E, other.E))));
        }

        public override int GetHashCode() {
          int hashcode = 157;
          unchecked {
            if(__isset.success)
            {
              hashcode = (hashcode * 397) + Success.GetHashCode();
            }
            if((E != null) && __isset.e)
            {
              hashcode = (hashcode * 397) + E.GetHashCode();
            }
          }
          return hashcode;
        }

        public override string ToString()
        {
          var tmp33 = new StringBuilder("Evaluate_result(");
          int tmp34 = 0;
          if(__isset.success)
          {
            if(0 < tmp34++) { tmp33.Append(", "); }
            tmp33.Append("Success: ");
            Success.ToString(tmp33);
          }
          if((E != null) && __isset.e)
          {
            if(0 < tmp34++) { tmp33.Append(", "); }
            tmp33.Append("E: ");
            E.ToString(tmp33);
          }
          tmp33.Append(')');
          return tmp33.ToString();
        }
      }


      public partial class SayHello_args : TBase
      {
        private string _name;

        public string Name
        {
          get
          {
            return _name;
          }
          set
          {
            __isset.name = true;
            this._name = value;
          }
        }


        public Isset __isset;
        public struct Isset
        {
          public bool name;
        }

        public SayHello_args()
        {
        }

        public SayHello_args DeepCopy()
        {
          var tmp35 = new SayHello_args();
          if((Name != null) && __isset.name)
          {
            tmp35.Name = this.Name;
          }
          tmp35.__isset.name = this.__isset.name;
          return tmp35;
        }

        public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
          iprot.IncrementRecursionDepth();
          try
          {
            TField field;
            await iprot.ReadStructBeginAsync(cancellationToken);
            while (true)
            {
              field = await iprot.ReadFieldBeginAsync(cancellationToken);
              if (field.Type == TType.Stop)
              {
                break;
              }

              switch (field.ID)
              {
                case 1:
                  if (field.Type == TType.String)
                  {
                    Name = await iprot.ReadStringAsync(cancellationToken);
                  }
                  else
                  {
                    await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  }
                  break;
                default: 
                  await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                  break;
              }

              await iprot.ReadFieldEndAsync(cancellationToken);
            }

            await iprot.ReadStructEndAsync(cancellationToken);
          }
          finally
          {
            iprot.DecrementRecursionDepth();
          }
        }

        public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
          oprot.IncrementRecursionDepth();
          try
          {
            var tmp36 = new TStruct("SayHello_args");
            await oprot.WriteStructBeginAsync(tmp36, cancellationToken);
            var tmp37 = new TField();
            if((Name != null) && __isset.name)
            {
              tmp37.Name = "name";
              tmp37.Type = TType.String;
              tmp37.ID = 1;
              await oprot.WriteFieldBeginAsync(tmp37, cancellationToken);
              await oprot.WriteStringAsync(Name, cancellationToken);
              await oprot.WriteFieldEndAsync(cancellationToken);
            }
            await oprot.WriteFieldStopAsync(cancellationToken);
            await oprot.WriteStructEndAsync(cancellationToken);
          }
          finally
          {
            oprot.DecrementRecursionDepth();
          }
        }

        public override bool Equals(object that)
        {
          if (!(that is SayHello_args other)) return false;
          if (ReferenceEquals(this, other)) return true;
          return ((__isset.name == other.__isset.name) && ((!__isset.name) || (global::System.Object.Equals(Name, other.Name))));
        }

        public override int GetHashCode() {
          int hashcode = 157;
          unchecked {
            if((Name != null) && __isset.name)
            {
              hashcode = (hashcode * 397) + Name.GetHashCode();
            }
          }
          return hashcode;
        }

        public override string ToString()
        {
          var tmp38 = new StringBuilder("SayHello_args(");
          int tmp39 = 0;
          if((Name != null) && __isset.name)
          {
            if(0 < tmp39++) { tmp38.Append(", "); }
            tmp38.Append("Name: ");
            Name.ToString(tmp38);
          }
          tmp38.Append(')');
          return tmp38.ToString();
        }
      }

    }

  }
}
