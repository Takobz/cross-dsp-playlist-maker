export type AuthorizationCodeFlowURLResponse = {
    data: {
        authorization_code_flow_redirect: string;
        authorization_state: string;
    },
    error_messages: string[];
}