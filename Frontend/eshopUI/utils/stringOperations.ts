export function lowercaseKeys(obj: any): any {
  if (Array.isArray(obj)) {
    return obj.map(lowercaseKeys);
  } else if (obj !== null && typeof obj === 'object') {
    return Object.keys(obj).reduce((acc: any, key: string) => {
      const lowerKey = key.charAt(0).toLowerCase() + key.slice(1);
      acc[lowerKey] = lowercaseKeys(obj[key]);
      return acc;
    }, {});
  }
  return obj;
}