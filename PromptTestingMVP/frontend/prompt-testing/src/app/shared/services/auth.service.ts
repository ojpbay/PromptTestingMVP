// T052 OIDC auth integration placeholder
export class AuthService {
  private token: string | null = null;
  async signInSilent(): Promise<void> {
    this.token = 'dev-token';
  }
  getToken() { return this.token; }
}