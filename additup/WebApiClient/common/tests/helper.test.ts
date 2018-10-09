import * as mocha from 'mocha';
import * as chai from 'chai';
import { Helper } from '../src'
// To execute the tests below "npm run tests"

describe('Helper Methods', () => {
    let helper: Helper;

    beforeEach(function () {
        helper = new Helper();
    });

    it('should return false when validating empty string', () => {
        let result = helper.isInteger("");
        chai.assert.isFalse(result);
    });

    it('should return false when validating string containing alphabets', () => {
        let result = helper.isInteger("abc");
        chai.assert.isFalse(result);
    });

    it('should return false when validating string containing alphanumberic values', () => {
        let result = helper.isInteger("abc123");
        chai.assert.isFalse(result);
    });

    it('should return false when validating string containing decimal value', () => {
        let result = helper.isInteger("1.2");
        chai.assert.isFalse(result);
    });

    it('should return true when validating string containing only numbers', () => {
        let result = helper.isInteger("123");
        chai.assert.isTrue(result);
    });

    it('should return true when validating string containing negative value', () => {
        let result = helper.isInteger("-5");
        chai.assert.isTrue(result);
    });

});