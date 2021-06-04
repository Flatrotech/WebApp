/**
 * @copyright Copyright 2021 Brendan Nason <flatrotech@gmail.com>
 * @license MIT
 * @module modulename
 */

'use strict';

module.exports =
async function func(options) {
  if (options !== undefined && typeof options !== 'object') {
    throw new TypeError('options must be an object');
  }

  // Do stuff
};
