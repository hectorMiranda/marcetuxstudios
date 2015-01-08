// ES6 class compiled to ES5 via Babel in the Gulp pipeline.
// Arrow functions and destructuring included — all ES5 in the output.
'use strict';

class ProfileFilter {
  constructor({ minAge, maxAge, gender, geoBucket }) {
    this.minAge    = minAge;
    this.maxAge    = maxAge;
    this.gender    = gender;
    this.geoBucket = geoBucket;
  }

  matches(profile) {
    const { age, gender, geoBucket } = profile;
    return (
      age >= this.minAge &&
      age <= this.maxAge &&
      gender === this.gender &&
      geoBucket === this.geoBucket
    );
  }

  toViewKey() {
    return [this.geoBucket, this.minAge, this.gender];
  }
}

module.exports = ProfileFilter;
