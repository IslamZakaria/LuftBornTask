import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'LuftBornTask',
    logoUrl: 'assets/images/logo/luftborn.png',
  },
  oAuthConfig: {
    issuer: 'http://localhost:5000/',
    redirectUri: baseUrl,
    clientId: 'LuftBornTask_App',
    responseType: 'code',
    scope: 'offline_access LuftBornTask',
    requireHttps: false
  },
  apis: {
    default: {
      url: 'http://localhost:5000',
      rootNamespace: 'LuftBornTask',
    },
  },
} as Environment;
