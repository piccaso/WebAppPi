using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace WebAppPi.Services
{
    public class MqttService : IHostedService {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;
        private MqttClient _client;
        private Timer _timer;

        public MqttService(ILogger<MqttService> logger, AppSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");
            MqttUp();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            MqttDown();
            return Task.CompletedTask;
        }

        public void MqttUp()
        {
            _client = new MqttClient(_settings.MqttHost,_settings.MqttPort, false, null, null, MqttSslProtocols.None);
            _client.Connect(Guid.NewGuid().ToString().Replace("-",""));
            _client.MqttMsgPublishReceived += ClientOnMqttMsgPublishReceived;
            _client.Subscribe(new[] { _settings.MqttTopic }, new[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _timer = new Timer(Callback, null, 0, 1000*60*10);
        }

        private void Callback(object state)
        {
            _client.Publish(_settings.MqttTopic, Encoding.UTF8.GetBytes($"{DateTime.Now}"));
        }

        public void MqttDown()
        {
            if (_client != null)
            {
                _client.MqttMsgPublishReceived -= ClientOnMqttMsgPublishReceived;
                _client.Disconnect();
                _client = null;
                _timer.Dispose();
            }
        }

        private void ClientOnMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs eventArgs)
        {
            try
            {
                var msg = Encoding.UTF8.GetString(eventArgs.Message);
                _logger.LogInformation($"{eventArgs.Topic}: {msg}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(ClientOnMqttMsgPublishReceived));
            }
        }
    }
}
