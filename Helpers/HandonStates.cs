namespace TaskPro1.Helpers
{
    public enum HandonStates
    {
        Initial,
        SearchingOrigin,
        SearchingDestination,
        CalculatingRoute,
        ChoosingRide,
        ConfirmingPickUp,
        ShowingXUberPass,
        ShowingHealthyMeasures,
        AssigningDriver,
        WaitingForDriver,
        TripInProgress,
        TripCompleted
    }
}