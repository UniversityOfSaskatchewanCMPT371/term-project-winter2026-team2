using System.Runtime.CompilerServices;
// required since player service has its own defined internal method
[assembly: InternalsVisibleTo("PlayerServiceEditMode")]
[assembly: InternalsVisibleTo("PlayerServicePlayMode")]