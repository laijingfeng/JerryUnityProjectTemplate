#import <Foundation/Foundation.h>

@interface JerrySDKBridge : NSObject
@end

#if defined (__cplusplus)
extern "C"
{
#endif
    char* makeStringCopy (const char* string);
    extern char* __getIDFA();

#if defined (__cplusplus)
}
#endif