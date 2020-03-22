import NodeRSA from './node_modules/node-rsa';
const key = new NodeRSA();
key.generateKeyPair();
let prvKey = key.exportKey('private');
let pubKey = key.exportKey('public')


console.log('public; ' + pubKey)
console.log('private; ' + prvKey)



