from http.server import HTTPServer, SimpleHTTPRequestHandler
import os

class SecurityHeadersHTTPRequestHandler(SimpleHTTPRequestHandler):
    def end_headers(self):
        # CSP ESTRICTA - Sin 'unsafe-inline' pero permitiendo recursos locales
        csp = (
            "default-src 'self'; "
            "script-src 'self'; "  
            "style-src 'self'; " 
            "img-src 'self' data:; "
            "font-src 'self' data:; "
            "connect-src 'self' https://localhost:7103; "
            "object-src 'none'; "
            "frame-ancestors 'none';"
        )
        
        # Headers de seguridad recomendados por OWASP
        self.send_header('Content-Security-Policy', csp)
        self.send_header('X-Frame-Options', 'DENY')
        self.send_header('X-Content-Type-Options', 'nosniff')
        self.send_header('X-XSS-Protection', '1; mode=block')
        self.send_header('Referrer-Policy', 'strict-origin-when-cross-origin')
        self.send_header('Permissions-Policy', 'geolocation=(), microphone=(), camera=()')
        
        super().end_headers()

if __name__ == '__main__':
    web_dir = os.path.dirname(os.path.abspath(__file__))
    os.chdir(web_dir)
    
    server_address = ('localhost', 8000)
    httpd = HTTPServer(server_address, SecurityHeadersHTTPRequestHandler)
    
    print('=' * 60)
    print('🔒 Servidor con Headers de Seguridad OWASP')
    print('=' * 60)
    print('URL: http://localhost:8000')
    print('\n📋 Headers de Seguridad Activos:')
    print('  ✓ Content-Security-Policy (CSP)')
    print('  ✓ X-Frame-Options: DENY (Anti-ClickJacking)')
    print('  ✓ X-Content-Type-Options: nosniff')
    print('  ✓ X-XSS-Protection: 1; mode=block')
    print('  ✓ Referrer-Policy: strict-origin-when-cross-origin')
    print('  ✓ Permissions-Policy')
    print('\n⚡ Presiona Ctrl+C para detener el servidor\n')
    print('=' * 60)
    
    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        print('\n\n✓ Servidor detenido correctamente')
