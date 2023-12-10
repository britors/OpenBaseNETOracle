using Oracle.ManagedDataAccess.Client;

namespace OpenBaseNET.Infra.Resilience.Database.Oracle.ExceptionPredicate;

internal static class OracleExceptionPredicate
{
    internal static bool ShouldRetryOn(OracleException exception)
    {
        return (from OracleError error in exception.Errors
            select error.Number switch
            {
                Deadlock
                    or ServerConnectionLost
                    or ConnectionLost
                    or TnsListenerDoesNotCurrentlyKnowOfServiceRequestedInConnectDescriptor
                    or TnsListenerCouldNotHandOffClientConnection
                    or ResourceBusy
                    or Timeout => true,
                _ => false
            }).FirstOrDefault(result => result);
    }

    #region constantes

    private const int Deadlock = 60;
    private const int ServerConnectionLost = 3113;
    private const int ConnectionLost = 3114;
    private const int TnsListenerDoesNotCurrentlyKnowOfServiceRequestedInConnectDescriptor = 12514;
    private const int TnsListenerCouldNotHandOffClientConnection = 12518;
    private const int ResourceBusy = 54;
    private const int Timeout = 12170;

    #endregion constantes
}