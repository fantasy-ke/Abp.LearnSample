 import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44305/',
  redirectUri: baseUrl,
  clientId: 'Blog_App',
  responseType: 'code',
  scope: 'offline_access Blog',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Blog',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44305',
      rootNamespace: 'Blog',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
