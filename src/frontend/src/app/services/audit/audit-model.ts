export interface AuditDto {
  id: number;
  method: string;
  path: string;
  body: string | null;
  statusCode: number;
  durationMs: number;
  error: string | null;
  headersJson: string | null;
  queryParamsJson: string;
  paramsJson: string;
  createdAt: string;
}
