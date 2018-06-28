#import "JerrySDKBridge.h"
#import <AdSupport/AdSupport.h>

@implementation JerrySDKBridge
@end

#if defined (__cplusplus)
extern "C"
{
#endif
	char* makeStringCopy (const char* string){
        if (string == NULL){
            return NULL;
		}
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }
	
	char* __getIDFA(){
		NSString *str = [[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString];
		return makeStringCopy([str UTF8String]);
	}

#if defined (__cplusplus)
}
#endif