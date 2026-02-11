import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'LuftBornTask',
    logoUrl: 'assets/images/logo/luftborn.png',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44350/',
    redirectUri: baseUrl,
    clientId: 'LuftBornTask_App',
    responseType: 'code',
    scope: 'offline_access LuftBornTask',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44350',
      rootNamespace: 'LuftBornTask',
    },
  },
} as Environment;
