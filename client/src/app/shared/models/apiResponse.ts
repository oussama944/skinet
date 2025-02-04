export interface ApiResponse<T> {
    value: T;
    formatters: any[];
    contentTypes: any[];
    declaredType: any;
    statusCode: number;
  }
  