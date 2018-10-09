import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AppErrorHandler implements ErrorHandler {

    constructor(private toastr: ToastrService, private zone: NgZone) { }

    /**
     * Unhandled exception handler - i.e. the implementation of ErrorHandler interface.
     */
    handleError(err: any) {
        // Ensure we're running this inside the angular zone - otherwise thrown errors
        // can be outside Angular's zone which causes problems when displaying them.
        this.zone.run(() => {
            // General handling of any other uncaught errors.
            this.log(err);
            this.toastr.warning(`Code: ${err.status} <br/> Message: ${err.message} <br/> Url: ${err.url}`,
            "Error!",
            {closeButton: true, disableTimeOut: true, enableHtml: true, positionClass: 'toast-bottom-full-width'}
            );
        });
    }

    log(err) {
        console.error(`UNHANDLED general error encountered: ${err.status} - ${err.message}`);
    }
}