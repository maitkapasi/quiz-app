export class Helper {
    isInteger(value: string): boolean {
        if(value.length > 0) {
            let numberValue: number = Number(value);
            if(!isNaN(numberValue) && Math.round(numberValue) === numberValue) {
                return true;
            }
        }

        return false;
        //return typeof value === 'number' &&  Math.floor(value) === value;
        //return value.length > 0 && !isNaN(Number(value)) && Math.round(value) === value;
    }
}

