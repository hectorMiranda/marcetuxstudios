// ES2015 module syntax — named exports, Babel + Webpack bundle.
// Each exported function is visible at declaration, not buried at end of file.

const GEO_BUCKETS = ['la', 'sf', 'sd', 'nyc'];

export function geoBucket(city) {
  const normalized = city.toLowerCase().replace(/\s+/g, '-');
  return GEO_BUCKETS.find(b => normalized.includes(b)) || 'other';
}

export function ageRange(age) {
  const bucket = Math.floor(age / 5) * 5;
  return [bucket, bucket + 4];
}

export class ProfileFilter {
  constructor({ minAge, maxAge, gender, geoBucket }) {
    this.minAge    = minAge;
    this.maxAge    = maxAge;
    this.gender    = gender;
    this.geoBucket = geoBucket;
  }

  matches({ age, gender, location }) {
    return (
      age >= this.minAge &&
      age <= this.maxAge &&
      gender === this.gender &&
      geoBucket(location) === this.geoBucket
    );
  }
}
