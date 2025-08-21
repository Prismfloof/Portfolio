# Lab Report – 2025-08-20

## Objective
Set up an Ubuntu Server 24.04 LTS system with a static IP, establish reliable network connectivity, and deploy Pi-hole as a network-wide DNS server with ad-blocking and DNSSEC enabled.

## System Installation and Network Configuration
1. Installed **Ubuntu Server 24.04 LTS** on a Raspberry Pi 3B.  
2. Attempted automatic Wi-Fi configuration, but encountered errors. 
   - Created a custom Netplan YAML configuration to establish Wi-Fi connectivity and assign a static IP.  
3. Reserved **192.168.1.100** for the server in the router’s LAN settings.  

**Observations:**  
- Server initially reported both the old IP **192.168.1.30** and the new static IP **192.168.1.100**.  
- SSH access was unavailable on the new IP until a server restart.  
- Placed `99-disable-network-config.cfg` in `/etc/cloud/cloud.cfg.d` to prevent cloud-init from managing the network.

## External Network Connectivity
- Initial ping attempts to **8.8.8.8** and **google.com** failed.  
- Manually added default gateway using:  
  ```bash
  sudo ip route add default via 192.168.1.1 dev wlan0
- Verified external connectivity after this change.

## Pi-hole Deployment and Configuration
1. Installed Pi-hole and verified web interface access.
2. Configured Pi-hole with:
   - Default block list
   - Additional recommended block list
   - Upstream external DNS: **8.8.8.8**
   - DNSSEC enabled
3. Assigned Pi-hole as the DNS server for:
   - Desktop client (verified queries routed correctly)
   - Router as primary DNS, fallback to **8.8.8.8**

**Observations:**
- Server initially reported old IP **192.168.1.30**; removal of auto-generated Netplan file resolved this.
- Switching from depreciated `gateway4` to route-based configuration temporarily broke Pi-hole access; server restart restored functionality.

## Lessons Learned
- Direct DHCP assignment by Pi-hole is important for accurate per-device DNS logging.
- Manual network configuration requires careful handling of default gateways and cloud-init overrides.
- Raspberry Pi + static IP + Pi-hole setup works but requires monitoring network stability.
## Next Steps
- [ ] Monitor server and Pi-hole logs for per-device visibility.
- [ ] Review firewall rules to ensure Pi-hole and SSH are not blocked.
- [ ] Consider containerizing Pi-hole for easier updates and isolation.
- [ ] Use DHCP reservations or Pi-hole DHCP to simplify device management.
- [ ] Expand block lists and explore monitoring dashboards (e.g., Grafana) for long-term network analysis.
- [ ] Configure Pi-hole to work with Unbound.
- [ ] Switch to a more robust BIND solution for more advanced networking concepts.